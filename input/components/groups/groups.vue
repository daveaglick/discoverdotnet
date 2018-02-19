<template>
    <div>            
        <card-grid
            title="Groups"
            suggest-link="/suggest/group"
            card-json="/data/groups.json"
            :filters="filters"
            :sorts="sorts"
            :randomizeOrder="true">
        </card-grid>
    </div>
</template>

<script>  
    module.exports = {
        data: function() {
            return {
                filters: [
                ],
                sorts: [
                ]
            }
        }
    }
</script>
