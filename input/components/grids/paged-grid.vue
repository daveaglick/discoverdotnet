<template>
    <div>
        <a id="card-grid-anchor"></a>
        
        <loading :data="pagedCardData"></loading>

        <div class="row">
            <div v-for="cardData in pagedCardData" :key="cardData" :class="columnClasses" class="mb-4 full-height-card">
                <slot :card-data="cardData">
                    <component :is="getCard(cardData)" :card-data="cardData"></component>
                </slot>
            </div>
        </div>

        <b-pagination
            v-if="totalCount > perPage"
            :total-rows="totalCount"
            v-model="currentPage"
            :per-page="perPage"
            :last-text="'&raquo; ' + totalCount + ' Total'"
            align="center"
            @input="pageChange">
        </b-pagination>
    </div>
</template>

<script>
    module.exports = {
        props: {
            cardJson: null,
            totalCount: null,
            perPage:
            {
                default: 24
            },
            columnClasses:
            {                
                default: "col-sm-6 col-md-4 col-lg-3 col-xl-2"
            }
        },
        data: function() {
            return {
                pagedCardData: [],
                currentPage: 1
            }
        },
        mounted: function() {
            this.populateCardData(false);
        },
        methods: {
            pageChange: function() {
                this.populateCardData(true);
            },
            populateCardData: function(scroll) {                
                axios
                    .get(this.cardJson + (this.currentPage - 1) + ".json")
                    .then(response => {
                        this.pagedCardData = response.data;
                        if(scroll) {
                            Vue.nextTick(function () {
                                document.getElementById('card-grid-anchor').scrollIntoView({ behavior: "smooth" });
                            });
                        }
                    })
                    .catch(e => {
                        console.log(e);
                    });
            }
        }
    }
</script>