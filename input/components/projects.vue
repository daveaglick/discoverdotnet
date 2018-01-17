<template>
    <div>
        <div class="text-center">        
            <h1>
                <span class="accent-secondary">All</span>
                <span class="accent-primary">Projects</span>
            </h1>
            <hr />        
            <h5 class="accent-script">
                {{ filteredProjects.length }} Projects
                <small v-if="filteredProjects.length != projects.length">(Of {{ projects.length }} Total)</small>
            </h5>
            <p><a href="/suggest/project">Suggest A Project</a></p>
        </div>
        <b-navbar toggleable="md" variant="dark" type="dark" class="my-4">
            <b-navbar-toggle target="filter-nav-collapse"></b-navbar-toggle>
            <b-collapse is-nav id="filter-nav-collapse">
                <b-navbar-nav>      
                    <filter-dropdown text="Language" :values="mapProjects('language').concat('F#')" :selected.sync="selectedLanguages"></filter-dropdown>
                    <filter-dropdown text="Tags" :values="mapProjects('tags')" :selected.sync="selectedTags"></filter-dropdown>
                    <b-nav-item-dropdown text="Sort">
                        <b-dropdown-item>Name</b-dropdown-item>
                        <b-dropdown-item>Most Stars</b-dropdown-item>
                        <b-dropdown-item>Least Stars</b-dropdown-item>
                    </b-nav-item-dropdown>
                </b-navbar-nav>
                <b-navbar-nav class="ml-auto">
                    <b-nav-form>
                        <b-button size="sm" class="mr-sm-2 mt-2 mt-sm-0" @click="shuffle">Shuffle The Deck</b-button>
                        <div class="w-100 mb-2 d-lg-none"></div>
                        <b-button size="sm">Random Draw</b-button>
                    </b-nav-form>
                </b-navbar-nav>
            </b-collapse>
        </b-navbar>
        <cards :cards-data="filteredProjects"></cards>
    </div>
</template>

<script>  
 module.exports = {
    data: function() {
        return {
            projects: [],
            languageFilter: null,
            tagsFilter: null,
            selectedTags: [],
            selectedLanguages: []
        }
    },
    created: function() {
        axios
            .get('/data/projects.json')
            .then(response => {
                this.projects = response.data;
                this.projects = this.projects.reduce(function (res, current, index, array) {
                    return res.concat([current, current]);
                }, []);
            })
            .catch(e => {
                console.log(e);
            });
    },
    computed: {
        filteredProjects: function() {
            var filtered = this.projects;
            filtered = this.filter(filtered, this.selectedLanguages, 'language');
            filtered = this.filter(filtered, this.selectedTags, 'tags');
            return filtered;
        }
    },
    methods: {
        shuffle: function() {
            this.projects = this.projects.sort(function(){return 0.5 - Math.random()});
        },
        mapProjects: function(field) {
            return this.projects.map(function(item) {
                if(field in item) {
                    return item[field];
                }
            });
        },
        filter(filtered, selected, field) {
            if(selected.length > 0) {
                return filtered.filter(function(item) {
                    if(field in item) {
                        if(Array.isArray(item[field])) {
                            return selected.some(function(search) {
                                return item[field].indexOf(search) !== -1;
                            });
                        }
                        return selected.indexOf(item[field]) !== -1;
                    }
                    return false;
                });
            }
            return filtered;
        }
    }
}
</script>
