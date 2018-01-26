<template>
    <div>
        <div class="text-center">        
            <h1>
                <span class="accent-secondary">All</span>
                <span class="accent-primary">{{ title }}</span>
            </h1>
            <hr />        
            <h5 class="accent-script">
                {{ filteredCardData.length }} {{ title }}
                <small v-if="filteredCardData.length != cardData.length">(Of {{ cardData.length }} Total)</small>
            </h5>
        </div>

        <b-navbar toggleable="md" variant="dark" type="dark" class="my-4">
            <b-navbar-toggle target="filter-nav-collapse"></b-navbar-toggle>
            <b-collapse is-nav id="filter-nav-collapse">
                <b-navbar-nav>   
                    <filter-dropdown
                        v-for="(filter, index) in filters"
                        :key="filter.text"
                        :text="filter.text"
                        :values="filterValues(filter)"
                        :selected.sync="selected[index]">
                    </filter-dropdown>
                    <b-nav-item-dropdown v-if="sorts" text="Sort">
                        <b-dropdown-item v-for="sort in sorts" :key="sort.text" @click="applySort(sort)">
                            {{ sort.text }}
                        </b-dropdown-item>
                    </b-nav-item-dropdown>
                </b-navbar-nav>
                <b-navbar-nav class="ml-auto">
                    <b-nav-item :href="suggestLink">Suggest New</b-nav-item>
                    <b-nav-form>
                        <b-button size="sm" class="mr-sm-2 mt-2 mt-sm-0" @click="shuffle">Shuffle The Deck</b-button>
                        <div class="w-100 mb-2 d-lg-none"></div>
                        <b-button size="sm">Random Draw</b-button>
                        <div class="w-100 mb-2 d-lg-none"></div>
                    </b-nav-form>
                </b-navbar-nav>
            </b-collapse>
        </b-navbar>

        <cards :card-data="filteredCardData"></cards>
    </div>
</template>

<script>
    module.exports = {
        props: [
            'title',
            'suggestLink',
            'cardJson',
            'filters',
            'sorts'
        ],
        data: function() {
            return {
                cardData: [],
                // See https://stackoverflow.com/q/25512771/807064 
                selected: Array.apply(null, new Array(this.filters.length)).map(function(){ return new Array(); })
            }
        },
        created: function() {
            axios
                .get(this.cardJson)
                .then(response => {
                    this.cardData = response.data;
                    this.shuffle();
                })
                .catch(e => {
                    console.log(e);
                });
        },
        computed: {
            filteredCardData: function() {
                var selected = this.selected;
                var filtered = this.cardData;
                this.filters.forEach(function(filter, index) {
                    if('filter' in filter) {
                        // Call the specified filter function
                        filtered = filter['filter'](filtered, selected[index]);
                    } else {
                        // Filter by the specificed field prop
                        if(selected[index].length > 0) {
                            filtered = filtered.filter(function(item) {
                                if(filter.field in item) {
                                    if(Array.isArray(item[filter.field])) {
                                        return selected[index].some(function(search) {
                                            return item[filter.field].indexOf(search) !== -1;
                                        });
                                    }
                                    return selected[index].indexOf(item[filter.field]) !== -1;
                                }
                                return false;
                            });
                        }
                    }
                });
                return filtered;
            }
        },
        methods: {
            shuffle: function() {
                this.cardData = this.cardData.sort(function(){return 0.5 - Math.random()});
            },
            applySort: function(sortData) {
                this.cardData = this.cardData.sort(sortData.sort);
            },
            filterValues: function(filter) {
                // If raw values were provided, use those
                if('values' in filter) {
                    return filter['values'];
                }

                // Otherwise, map the field values from the data
                return this.cardData.map(function(item) {
                    if(filter.field in item) {
                        return item[filter.field];
                    }
                });
            }
        }
    }
</script>