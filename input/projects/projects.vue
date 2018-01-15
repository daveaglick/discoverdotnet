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
                </b-navbar-nav>
            </b-collapse>
        </b-navbar>
        <b-card-group deck v-for="i in Math.ceil(projects.length / 4)" :key="i" class="mb-4">
            <project v-if="(i - 1) * 4 < projects.length" :card-data="projects[(i - 1) * 4]"></project>
            <b-card v-else class="invisible"></b-card>            
            <project v-if="((i - 1) * 4) + 1 < projects.length" :card-data="projects[((i - 1) * 4) + 1]"></project>
            <b-card v-else class="invisible"></b-card>            
            <div class="w-100 mb-4 d-lg-none"></div>
            <project v-if="((i - 1) * 4) + 2 < projects.length" :card-data="projects[((i - 1) * 4) + 2]"></project>
            <b-card v-else class="invisible"></b-card>            
            <project v-if="((i - 1) * 4) + 3 < projects.length" :card-data="projects[((i - 1) * 4) + 3]"></project>
            <b-card v-else class="invisible"></b-card>            
        </b-card-group>
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
    }
}
</script>
