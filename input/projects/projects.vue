<template>
    <div>
        <div class="text-center">        
            <h1>
                <span class="accent-secondary">All</span>
                <span class="accent-primary">Projects</span>
            </h1>
            <hr />        
            <h5 class="accent-script">1234 Total Projects</h5>
            <p><a href="/suggest/project">Suggest A Project</a></p>
        </div>
        <b-navbar toggleable="md" variant="dark" type="dark" class="my-4">
            <b-navbar-toggle target="filter-nav-collapse"></b-navbar-toggle>
            <b-collapse is-nav id="filter-nav-collapse">
                <b-navbar-nav>      
                    <b-nav-item-dropdown text="Language">
                        <b-dropdown-item>C#</b-dropdown-item>
                        <b-dropdown-item>F#</b-dropdown-item>
                    </b-nav-item-dropdown>
                    <b-nav-item-dropdown text="Tag">
                        <b-dropdown-item>static sites</b-dropdown-item>
                        <b-dropdown-item>build tools</b-dropdown-item>
                    </b-nav-item-dropdown>
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
            projects: []
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
            return this.projects;
        }
    },
    methods: {
        shuffle: function() {
            // From https://stackoverflow.com/a/12646864/807064
            for (var i = this.projects.length - 1; i > 0; i--) {
                var j = Math.floor(Math.random() * (i + 1));
                var temp = this.projects[i];
                this.projects[i] = this.projects[j];
                this.projects[j] = temp;
            }
            console.log(this.projects);
        }
    }
}
</script>
