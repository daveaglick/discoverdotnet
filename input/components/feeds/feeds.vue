<template>
    <div>            
        <card-grid
            :title="title"
            :suggest-link="suggestLink"
            :card-json="cardJson"
            :sorts="sorts"
            :randomizeOrder="true">
        </card-grid>
    </div>
</template>

<script>  
    module.exports = {
        props: [
            'title',
            'suggestLink',
            'cardJson'
        ],
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
                    },
                    {
                        text: 'Recently Published',
                        sort: function(a, b) {
                            if(moment(a.lastPublished).isBefore(moment(b.lastPublished))) return 1;   
                            if(moment(b.lastPublished).isBefore(moment(a.lastPublished))) return -1;
                            return 0;              
                        }
                    }
                ]
            }
        }
    }
</script>
