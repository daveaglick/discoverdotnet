<template>
    <div>
        <card-grid
            :card-json="issueJson"
            :filters="filters"
            :sorts="sorts"
            column-classes="col-sm-12 col-md-6 col-lg-4">            
            <issue-card slot-scope="props" :card-data="props.cardData">
            </issue-card>
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
                    },
                    {
                        text: "Labels",
                        field: "labels"
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