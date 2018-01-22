<template>
    <div>            
        <card-grid
            title="Projects"
            suggest-link="/suggest/project"
            card-json="/data/projects.json"
            :filters="filters"
            :sorts="sorts">
        </card-grid>
    </div>
</template>

<script>  
 module.exports = {
    data: function() {
        return {
            filters: [
                {
                    text: 'Language',
                    field: 'language' // or values: ['a', 'b', 'c']
                    //filter: function(items, selected) {} // for custom filter 
                },
                {
                    text: 'Tags',
                    field: 'tags'
                }
            ],
            sorts: [
                {
                    text: 'Most Stars',
                    sort: function(a, b) {
                        if('stargazersCount' in a && 'stargazersCount' in b) {
                            return b.stargazersCount - a.stargazersCount;
                        } else if('stargazersCount' in a) {
                            return -1;
                        }    
                        return 1;                    
                    }
                },
                {
                    text: 'Least Stars',
                    sort: function(a, b) {
                        if('stargazersCount' in a && 'stargazersCount' in b) {
                            return a.stargazersCount - b.stargazersCount;
                        } else if('stargazersCount' in a) {
                            return 1;
                        }    
                        return -1;                    
                    }
                }
            ]
        }
    }
}
</script>
