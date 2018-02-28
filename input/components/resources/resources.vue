<template>
    <div>           
        <card-grid
            title="Resources"
            suggest-link="/suggest/resource"
            card-json="/data/resources.json"
            :sorts="sorts">
        </card-grid>
    </div>
</template>

<script>  
    module.exports = {
        data: function() {
            return {       
                sorts: [                    
                    {
                        text: 'A-Z',
                        sort: function(a, b) {
                            return a.title.localeCompare(b.title);
                        }
                    },
                    {
                        text: 'Z-A',
                        sort: function(a, b) {      
                            return b.title.localeCompare(a.title);     
                        }
                    }
                ]
            }
        }
    }
</script>
