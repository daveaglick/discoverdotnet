<template>
    <div>
        <help-wanted :project-keys="projectKeys"></help-wanted>
        <recent-issues :project-keys="projectKeys"></recent-issues>
    </div>
</template>


<script>
    module.exports = {
        data: function() {
            return {
                projectKeys: []
            }
        },
        created: function() {
            axios
                .get('/data/project-keys.json')
                .then(response => {
                    this.projectKeys = response.data
                })
                .catch(e => {
                    console.log(e);
                });
        }
    }
</script>