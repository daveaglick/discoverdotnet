<template>
    <div>           
        <card-grid
            title="Resources"
            suggest-link="/suggest/resource"
            card-json="/data/resources.json"
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
                        text: 'Tags',
                        field: 'tags'
                    }
                ],       
                sorts: [                    
                    {
                        text: 'A-Z',
                        sort: function(a, b) {
                            return a.title ? a.title.localeCompare(b.title) : -1;
                        }
                    },
                    {
                        text: 'Z-A',
                        sort: function(a, b) {      
                            return b.title ? b.title.localeCompare(a.title) : -1;
                        }
                    }
                ]
            }
        }
    }
</script>
