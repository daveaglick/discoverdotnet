<template>
    <div>
        <b-card-group deck v-for="i in Math.ceil(projects.length / 4)" :key="i">
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
            })
            .catch(e => {
                console.log(e);
            });
    }
}
</script>
