<template>
    <div>
        <card-grid
            title="issues"
            card-json="/data/issues/all.json"
            :filters="filters"
            :sorts="sorts">
            <small-card slot-scope="props" icon="fa-bug">
                <div class="font-weight-bold"><a :href="props.cardData.link">{{ props.cardData.title }}</a></div>
                <div><a :href="getProject(props.cardData).link">{{ getProject(props.cardData).title }}</a></div>
                <div slot="footer">
                    <div class="small">Created {{ props.cardData.createdAt | from-now }}</div>
                </div>
            </small-card>
        </card-grid>
    </div>
</template>


<script>
    module.exports = {
        data: function() {
            return {
                projectKeys: [],
                filters: [
                    {
                        text: "Recent",
                        filter: function(items, selected) {
                            debugger;
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
                    }
                ]
            }
        },
        created: function() {
            axios
                .get('/data/project-keys.json')
                .then(response => {
                    this.projectKeys = response.data
                })
                .catch(e => {
                    console.log(e);
                });
        },
        methods: {
            getProject: function(issue) {
                return this.projectKeys.find(function(item) {
                    return item.key == issue.projectKey;
                });
            }
        }
    }
</script>