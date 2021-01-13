<template>
    <b-nav-form>
        <b-form inline action="/search">
            <b-form-input
                id="query-input"
                name="query"
                type="search"
                placeholder="Search"
                v-model="query">
            </b-form-input>
        </b-form>
        <!-- I really want placement="bottomleft" but then it "flips" when too tall, this and size will force "flip" to bottom where I want it -->
        <b-popover
            ref="resultspopover"
            target="query-input"
            title="Search Results"
            placement="topleft"            
            triggers="blur"
            :show.sync="showResults">
            <search-query :query="query" :project-keys="projectKeys" :hits-per-page="5" :results-link="true"></search-query>
        </b-popover>
    </b-nav-form>
</template>

<script>
    module.exports = {
        props: [
            'projectKeys'
        ],
        computed: {
            showResults: function() {
                return this.query !== null && this.query.length > 0;
            }
        },
        data: function() {
            return {
                query: null
            }
        }
    }
</script>