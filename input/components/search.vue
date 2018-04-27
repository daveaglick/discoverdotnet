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
            container="main"
            triggers="blur"
            :show.sync="showResults"
            @hidden="onResultsHidden">
            <b-container class="search-results">
                <b-row>
                    <b-col sm v-if="(getResults('projects') !== null && getResults('projects').nbHits > 0) || (getResults('issues') !== null && getResults('issues').nbHits > 0)">
                        <search-results title="Projects" :results="getResults('projects')"> 
                            <template slot-scope="props">
                                <div><b-link :href="'/projects/' + props.result.key" class="font-weight-bold" v-html="props.result._highlightResult.title.value"></b-link></div>
                                <div class="small" v-html="props.result._highlightResult.description.value"></div>
                            </template>
                        </search-results>
                        <search-results title="Issues" :results="getResults('issues')"> 
                            <template slot-scope="props">
                                <div><b-link :href="props.result.link" class="font-weight-bold" v-html="props.result._highlightResult.title.value"></b-link>
                                <div v-if="getProject(props.result.projectKey)" class="small"><a :href="getProject(props.result.projectKey).link">{{ getProject(props.result.projectKey).title }}</a></div>
                            </template>
                        </search-results>
                    </b-col>
                    <b-col sm v-if="getResults('blogs') !== null && getResults('blogs').nbHits > 0">
                        <search-results title="Blogs" :results="getResults('blogs')"> 
                            <template slot-scope="props">
                                <div><b-link :href="'/' + props.result.key" class="font-weight-bold" v-html="props.result._highlightResult.title.value"></b-link></div>
                                <div class="small" v-html="props.result._highlightResult.description.value"></div>
                            </template>
                        </search-results>
                    </b-col>
                    <b-col sm v-if="getResults('broadcasts') !== null && getResults('broadcasts').nbHits > 0">
                        <search-results title="Broadcasts" :results="getResults('broadcasts')"> 
                            <template slot-scope="props">
                                <div><b-link :href="'/' + props.result.key" class="font-weight-bold" v-html="props.result._highlightResult.title.value"></b-link></div>
                                <div class="small" v-html="props.result._highlightResult.description.value"></div>
                            </template>
                        </search-results>
                    </b-col>
                    <b-col sm v-if="(getResults('groups') !== null && getResults('groups').nbHits > 0) || (getResults('resources') !== null && getResults('resources').nbHits > 0)">
                        <search-results title="Groups" :results="getResults('groups')"> 
                            <template slot-scope="props">
                                <div><b-link :href="props.result.website" class="font-weight-bold" v-html="props.result._highlightResult.title.value"></b-link></div>
                                <div class="small" v-html="props.result._highlightResult.location.value"></div>
                            </template>
                        </search-results>
                        <search-results title="Resources" :results="getResults('resources')"> 
                            <template slot-scope="props">
                                <div><b-link :href="props.result.website" class="font-weight-bold" v-html="props.result._highlightResult.title.value"></b-link></div>
                                <div class="small" v-html="props.result._highlightResult.description.value"></div>
                            </template>
                        </search-results>
                    </b-col>
                </b-row>
            </b-container>
        </b-popover>
    </b-nav-form>
</template>

<script>
    module.exports = {
        props: [
            'projectKeys'
        ],
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
            },            
            getResults: function(index) {
                if(this.results === null) {
                    return null;
                }
                return this.results.find(function(item) {
                    return item.index === index;
                });
            },            
            getProject: function(key) {
                if(this.projectKeys === null) {
                    return null;
                }
                return this.projectKeys.find(function(item) {
                    return item.key == key;
                });
            }
        }
    }
</script>