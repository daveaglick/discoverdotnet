<template>
    <div>
        <div v-if="title" class="text-center mb-4">        
            <h1>
                <span class="accent-secondary">All</span>
                <span class="accent-primary">{{ title }}</span>
            </h1>
        </div>

        <b-card no-body class="mb-4">            
            <slot name="header" :filtered-card-data="filteredCardData"></slot>
            <b-card-body>
                <div>
                    <span class="h5 font-weight-bold text-muted mr-2">Sort</span>                
                    <b-link href="#" v-b-toggle="'sort-collapse'" class="small">
                        <!-- Have to use d-none instead of v-if/v-else because Font Awesome SVGs don't rerender: https://stackoverflow.com/q/49343425/807064 -->
                        <span :class="{'d-none': !sortVisible}"><i class="far fa-minus-square"></i> Hide</span>
                        <span :class="{'d-none': sortVisible}"><i class="far fa-plus-square"></i> Show</span>
                    </b-link>
                </div>
                <b-collapse id="sort-collapse" v-model="sortVisible">
                    <div class="mt-3">
                        <span v-if="sorts.length > 0">
                            <b-button
                                v-for="sort in sorts"
                                :key="sort.text"
                                size="sm"
                                class="mb-2 mr-2"
                                variant="light"
                                @click="applySort(sort)">
                                {{ sort.text }}
                            </b-button><b-button size="sm" class="mb-2 mr-2" variant="light" @click="shuffle">Shuffle</b-button>
                        </span>
                    </div>
                </b-collapse>
            </b-card-body>
            <b-card-body v-if="filters">
                <div>
                    <span class="h5 font-weight-bold text-muted mr-2">Filters</span>        
                    <b-link href="#" v-b-toggle="'filters-collapse'" class="small">
                        <span :class="{'d-none': !filtersVisible}"><i class="far fa-minus-square"></i> Hide</span>
                        <span :class="{'d-none': filtersVisible}"><i class="far fa-plus-square"></i> Show</span>
                    </b-link>
                </div>
                <b-collapse id="filters-collapse" v-model="filtersVisible">
                    <div class="mt-3">
                        <span v-for="(filter, index) in filters" :key="filter.text">
                            <span v-if="'field' in filter || 'values' in filter">
                                <div>
                                    <filter-buttons
                                        :text="filter.text"
                                        :values="filterValues(filter)"
                                        :selected.sync="selected[index]">
                                    </filter-buttons>
                                </div>
                            </span>
                            <b-button
                                v-else
                                size="sm"
                                class="mb-2 mr-2"
                                :variant="selected[index] ? 'primary' : 'light'"
                                @click="filterItemClicked(index)">
                                {{ filter.text }}
                            </b-button>
                        </span>
                    </div>
                </b-collapse>
            </b-card-body>
            <b-card-body>
                <b-row>
                    <b-col>
                        <b-button v-if="filters" size="sm" @click="resetFilterSelection">Reset Filters</b-button>
                    </b-col>
                    <b-col class="text-center">
                        <h6>
                            <span v-if="filteredCardData.length != cardData.length">
                                {{ filteredCardData.length }} <small>(Of {{ cardData.length }} Total)</small>
                            </span>
                            <span v-else>{{ cardData.length }} Total</span>
                        </h6>
                    </b-col>
                    <b-col class="text-right">
                        <b-link v-if="suggestLink" :href="suggestLink">Suggest New</b-link>
                    </b-col>
                </b-row>
            </b-card-body>
        </b-card>
        
        <a id="card-grid-anchor"></a>

        <div class="row">
            <div v-for="cardData in pagedCardData" :key="cardData" :class="columnClasses" class="mb-4 full-height-card">
                <slot :card-data="cardData">
                    <component :is="getCard(cardData)" :card-data="cardData"></component>
                </slot>
            </div>
        </div>
            
        <b-pagination
            v-if="filteredCardData.length > perPage"
            :total-rows="filteredCardData.length"
            v-model="currentPage"
            :per-page="perPage"
            :last-text="'&raquo; ' + filteredCardData.length + ' Total'"
            align="center"
            @input="pageChange">
        </b-pagination>
    </div>
</template>

<script>
    module.exports = {
        props: {
            title: null,
            suggestLink: null,
            cardJson: null,
            filters: null,
            sorts: null,
            perPage:
            {
                default: 24
            },
            columnClasses:
            {                
                default: "col-sm-6 col-md-4 col-lg-3"
            },
            randomizeOrder: null
        },
        data: function() {
            return {
                cardData: [],
                selected: [],
                currentPage: 1,
                sortVisible: true,
                filtersVisible: true
            }
        },
        created: function() {
            var self = this;
            
            if(this.filters) {
                this.resetFilterSelection();

                // Preselect a filter from the query string
                var queryDict = {};
                location.search.substr(1).split("&").forEach(function(item) {
                    queryDict[item.split("=")[0]] = item.split("=")[1]
                });
                if('filter' in queryDict) {
                    var filterText = queryDict['filter'];
                    var filterIndex = this.filters.findIndex(function(item) {
                        return item.text.replace(/\W/g, '').toLowerCase() === filterText.toLowerCase();
                    });
                    if(filterIndex !== -1 && this.selected[filterIndex] === false) {
                        this.selected[filterIndex] = true;
                    }
                }
            }

            // Get the card data
            axios
                .get(this.cardJson)
                .then(response => {
                    if(this.randomizeOrder) {
                        this.cardData = response.data.sort(function(){return 0.5 - Math.random()});
                    } else {
                        this.cardData = response.data;
                    }
                })
                .catch(e => {
                    console.log(e);
                });
        },
        computed: {
            filteredCardData: function() {
                var selected = this.selected;
                var filtered = this.cardData;
                if(this.filters) {
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
                }

                return filtered;
            },
            pagedCardData: function() {
                var filtered = this.filteredCardData;
                
                if(filtered.length > this.perPage) {
                    filtered = filtered.slice((this.currentPage - 1) * this.perPage, this.currentPage * this.perPage);
                }

                return filtered;
            }
        },
        methods: {
            shuffle: function() {
                this.cardData = this.cardData.sort(function(){return 0.5 - Math.random()});
            },
            applySort: function(sortData) {
                if("apply" in sortData) {
                    var self = this;
                    sortData.apply(function() {
                        self.cardData = self.cardData.sort(sortData.sort);
                    });
                } else {
                    this.cardData = this.cardData.sort(sortData.sort);
                }
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
            },
            filterItemClicked: function(index) {
                Vue.set(this.selected, index, !this.selected[index]);
            },
            pageChange: function() {
                Vue.nextTick(function () {
                    document.getElementById('card-grid-anchor').scrollIntoView({ behavior: "smooth" });
                });
            },
            resetFilterSelection: function() {                
                // Prepare the filter selection array
                // See https://stackoverflow.com/q/25512771/807064 
                var self = this;
                this.selected = Array
                    .apply(null, new Array(this.filters.length))
                    .map(function(item, index){ 
                        var filter = self.filters[index];
                        if('field' in self.filters[index] || 'values' in self.filters[index])
                        {
                            return new Array(); 
                        } else {
                            return false;
                        }
                    });     
            }
        }
    }
</script>