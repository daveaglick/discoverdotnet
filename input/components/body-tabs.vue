<template>
    <b-tabs v-model="tabIndex" class="body-tabs">
        <slot></slot>
    </b-tabs>
</template>


<script>
    module.exports = {
        props: {
            // An array of strings used for selecting tabs from the query string
            querySelectors: null
        },
        data: function() {
            return {
                tabIndex: 0
            }
        },
        created: function() {
            var queryDict = {};
            location.search.substr(1).split("&").forEach(function(item) {
                queryDict[item.split("=")[0]] = item.split("=")[1]
            });
            if('tab' in queryDict && this.querySelectors !== null) {
                var tabText = queryDict['tab'];
                this.tabIndex = this.querySelectors.findIndex(function(item) {
                    return item.replace(/\W/g, '').toLowerCase() === tabText.toLowerCase();
                });
            }
        }
    }
</script>