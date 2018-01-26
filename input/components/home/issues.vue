<template>
    <div>
        <recent-issues :project-keys="projectKeys"></recent-issues>
        <help-wanted></help-wanted>
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