<template>
    <div>        
        <h1 class="font-weight-bold display-4">Search</h1>
        <b-form inline action="/search" class="text-light bg-dark p-3 mb-3 rounded">
            <span class="font-weight-bold mr-2">Search Query</span>
            <b-form-input
                id="query-input"
                name="query"
                type="search"
                placeholder="Search"
                v-model="query">
            </b-form-input>
        </b-form> 
        <b-card>
            <search-query :query="query" :project-keys="projectKeys" :hits-per-page="20"></search-query>
        </b-card>
    </div>
</template>

<script>
    module.exports = {
        props: [
            'projectKeys'
        ],
        data: function() {
            return {
                query: null
            }
        },
        mounted: function() {     
            var queryDict = {};
            location.search.substr(1).split("&").forEach(function(item) {
                queryDict[item.split("=")[0].toLowerCase()] = item.split("=")[1]
            });
            if('query' in queryDict) {
                this.query = decodeURIComponent(queryDict['query']).replace(/\+/g,  " ");
            }
        }
    }
</script>