<template>
    <div>            
        <card-grid
            :title="title"
            :suggest-link="suggestLink"
            :card-json="cardJson"
            :filters="filters"
            :sorts="sorts"
            :randomize-order="true">
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
                filters: [
                    {
                        text: 'Language',
                        field: 'language',
                        selected: 'English'
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
