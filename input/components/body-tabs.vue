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
        mounted: function() {
            var queryDict = {};
            location.search.substr(1).split("&").forEach(function(item) {
                queryDict[item.split("=")[0].toLowerCase()] = item.split("=")[1]
            });
            if('tab' in queryDict) {
                var tabText = queryDict['tab'];
                this.tabIndex = this.$children[0].$children.findIndex(function(item) {
                    return item.$props["title"].replace(/\W/g, '').toLowerCase() === tabText.toLowerCase();
                });
            }
        }
    }
</script>