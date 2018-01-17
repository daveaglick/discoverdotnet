<template>
    <div>            
        <card-grid title="Projects" suggest-link="/suggest/project" card-json="/data/projects.json" :filters="filters">
        </card-grid>
    </div>
</template>

<script>  
 module.exports = {
    data: function() {
        return {
            projects: []
        }
    },
    computed: {
        filters: function() {
            return [
                {
                    text: 'Language',
                    field: 'language' // or values:
                    //filter: function(items, selected) {} // for custom filter 
                },
                {
                    text: 'Tags',
                    field: 'tags'
                }
            ]
        }
    }
}
</script>
