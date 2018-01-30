<template>
    <div>
        <card-grid
            :card-json="issueJson"
            :filters="filters"
            :sorts="sorts"
            column-classes="col-sm-12 col-md-6 col-lg-4">
            <small-card slot-scope="props" icon="fa-bug">
                <div class="font-weight-bold"><a :href="props.cardData.link">{{ props.cardData.title }}</a></div>
                <div>
                    <b-badge v-for="label in props.cardData.labels" :key="label" variant="light" class="mr-2">{{ label }}</b-badge>
                </div>                
                <div slot="footer">
                    <div class="small">Created {{ props.cardData.createdAt | from-now }}</div>
                    <div class="small">Updated {{ props.cardData.updatedAt | from-now }}</div>
                </div>
            </small-card>
        </card-grid>
    </div>
</template>


<script>
    module.exports = {
        props: [
            'issueJson',
        ],
        data: function() {
            return {
                filters: [
                    {
                        text: "Recent",
                        filter: function(items, selected) {
                            if(selected) {
                                return items.filter(function(item) {
                                    return item.recent;
                                });
                            }
                            return items;
                        } 
                    },
                    {
                        text: "Help Wanted",
                        filter: function(items, selected) {
                            if(selected) {
                                return items.filter(function(item) {
                                    return item.helpWanted;
                                });
                            }
                            return items;
                        } 
                    }
                ],
                sorts: [
                    {
                        text: 'Newest',
                        sort: function(a, b) {
                            return moment(b.createdAt) - moment(a.createdAt);              
                        }
                    },
                    {
                        text: 'Oldest',
                        sort: function(a, b) {
                            return moment(a.createdAt) - moment(b.createdAt);                 
                        }
                    },
                    {
                        text: 'Recently Updated',
                        sort: function(a, b) {
                            return moment(b.updatedAt) - moment(a.updatedAt);              
                        }
                    }
                ]
            }
        }
    }
</script>