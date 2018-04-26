<template>
    <b-nav-form>
        <b-form-input
            id="query-input"
            type="search"
            placeholder="Search"
            v-model="query"
            @input="onQueryInput">
        </b-form-input>
        <b-popover
            target="query-input"
            title="Search Results"
            placement="bottomleft"
            triggers="blur"
            :show.sync="showResults"
            @hidden="onResultsHidden">
        </b-popover>
    </b-nav-form>
</template>

<script>
    module.exports = {
        data: function() {
            return {
                query: null,
                results: null
            }
        },
        computed: {
            showResults: function() {
                return this.results !== null;
            }
        },
        methods: {
            onQueryInput() {
                // Perform the search
                var client = algoliasearch('7TKEQH0O12', '7f1f96803cccf63f887a978008c9279d');
                var queries = [{
                    indexName: 'projects',
                    query: this.query,
                    params: {
                        hitsPerPage: 5
                    }
                }, {
                    indexName: 'issues',
                    query: this.query,
                    params: {
                        hitsPerPage: 10
                    }
                }, {
                    indexName: 'blogs',
                    query: this.query,
                    params: {
                        hitsPerPage: 5
                    }
                }, {
                    indexName: 'broadcasts',
                    query: this.query,
                    params: {
                        hitsPerPage: 5
                    }
                }, {
                    indexName: 'groups',
                    query: this.query,
                    params: {
                        hitsPerPage: 5
                    }
                }, {
                    indexName: 'resources',
                    query: this.query,
                    params: {
                        hitsPerPage: 5
                    }
                }];
                var self = this;
                client.search(queries, function (err, content) {
                    if (err) throw err;
                    console.log(content);
                    self.results = content.results;
                });
            },
            onResultsHidden() {
                this.results = null;
            }
        }
    }
</script>