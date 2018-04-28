<template>
    <b-nav-form>
        <b-form-input
            id="query-input"
            type="search"
            placeholder="Search"
            v-model="query"
            @input="onQueryInput">
        </b-form-input>
        <!-- I really want placement="bottomleft" but then it "flips" when too tall, this and size will force "flip" to bottom where I want it -->
        <b-popover
            ref="resultspopover"
            target="query-input"
            title="Search Results"
            placement="topleft"            
            triggers="blur"
            :show.sync="showResults">
            <b-container class="search-results">
                <b-row v-if="anyResults()">
                    <b-col sm v-if="(getIndexResults('projects') !== null && getIndexResults('projects').nbHits > 0) || (getIndexResults('issues') !== null && getIndexResults('issues').nbHits > 0)">
                        <search-results title="Projects" :results="getIndexResults('projects')"> 
                            <template slot-scope="props">
                                <div><b-link :href="'/projects/' + props.result.key" class="font-weight-bold" v-html="props.result._highlightResult.title.value"></b-link></div>
                                <div class="small" v-if="props.result._highlightResult.description" v-html="props.result._highlightResult.description.value"></div>
                            </template>
                        </search-results>
                        <search-results title="Issues" :results="getIndexResults('issues')"> 
                            <template slot-scope="props">
                                <div><b-link :href="props.result.link" class="font-weight-bold" v-html="props.result._highlightResult.title.value"></b-link>
                                <div v-if="getProject(props.result.projectKey)" class="small"><a :href="getProject(props.result.projectKey).link">{{ getProject(props.result.projectKey).title }}</a></div>
                            </template>
                        </search-results>
                    </b-col>
                    <b-col sm v-if="(getIndexResults('blogs') !== null && getIndexResults('blogs').nbHits > 0) || (getIndexResults('posts') !== null && getIndexResults('posts').nbHits > 0)">
                        <search-results title="Blogs" :results="getIndexResults('blogs')"> 
                            <template slot-scope="props">
                                <div><b-link :href="'/' + props.result.key" class="font-weight-bold" v-html="props.result._highlightResult.title.value"></b-link></div>
                                <div class="small" v-if="props.result._highlightResult.description" v-html="props.result._highlightResult.description.value"></div>
                            </template>
                        </search-results>
                        <search-results title="Posts" :results="getIndexResults('posts')"> 
                            <template slot-scope="props">
                                <div><b-link :href="props.result.link" class="font-weight-bold" v-html="props.result._highlightResult.title.value"></b-link></div>
                                <div class="small">{{ moment(props.result.published).format("LL") }} </div>
                            </template>
                        </search-results>
                    </b-col>
                    <b-col sm v-if="(getIndexResults('broadcasts') !== null && getIndexResults('broadcasts').nbHits > 0) || (getIndexResults('episodes') !== null && getIndexResults('episodes').nbHits > 0)">
                        <search-results title="Broadcasts" :results="getIndexResults('broadcasts')"> 
                            <template slot-scope="props">
                                <div><b-link :href="'/' + props.result.key" class="font-weight-bold" v-html="props.result._highlightResult.title.value"></b-link></div>
                                <div class="small" v-if="props.result._highlightResult.description" v-html="props.result._highlightResult.description.value"></div>
                            </template>
                        </search-results>
                        <search-results title="Episodes" :results="getIndexResults('episodes')"> 
                            <template slot-scope="props">
                                <div><b-link :href="props.result.link" class="font-weight-bold" v-html="props.result._highlightResult.title.value"></b-link></div>
                                <div class="small">{{ moment(props.result.published).format("LL") }} </div>
                            </template>
                        </search-results>
                    </b-col>
                    <b-col sm v-if="(getIndexResults('groups') !== null && getIndexResults('groups').nbHits > 0) || (getIndexResults('resources') !== null && getIndexResults('resources').nbHits > 0)">
                        <search-results title="Groups" :results="getIndexResults('groups')"> 
                            <template slot-scope="props">
                                <div><b-link :href="props.result.website" class="font-weight-bold" v-html="props.result._highlightResult.title.value"></b-link></div>
                                <div class="small" v-if="props.result._highlightResult.location" v-html="props.result._highlightResult.location.value"></div>
                            </template>
                        </search-results>
                        <search-results title="Resources" :results="getIndexResults('resources')"> 
                            <template slot-scope="props">
                                <div><b-link :href="props.result.website" class="font-weight-bold" v-html="props.result._highlightResult.title.value"></b-link></div>
                                <div class="small" v-if="props.result._highlightResult.description" v-html="props.result._highlightResult.description.value"></div>
                            </template>
                        </search-results>
                    </b-col>
                </b-row>
                <b-row v-else>
                    <b-col class="h-100">
                        <p>No results found.</p>
                    </b-col>
                </b-row>
                <b-row>
                    <b-col class="text-right">
                        <a href="https://www.algolia.com/?utm_source=aos&utm_medium=web&utm_campaign=discovernet">
                            <img src="https://www.algolia.com/static_assets/images/press/downloads/search-by-algolia.svg" style="width: 160px;">
                        </a>
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
                return this.query !== null && this.query.length > 0 && this.results !== null;
            }
        },
        methods: {
            onQueryInput() {
                if(this.query === null || this.query.length === 0) {
                    this.results = null;
                    return;
                }

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
                    indexName: 'posts',
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
                    indexName: 'episodes',
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
                    self.results = content.results;
                });
            },         
            anyResults: function() {
                if(this.results === null) {
                    return null;
                }
                return this.results.some(function(item) {
                    return item.nbHits > 0;
                });
            },        
            getIndexResults: function(index) {
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