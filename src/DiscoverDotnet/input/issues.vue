<template>
    <div>
        <projects :project-keys="projectKeys"></projects>
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
            var self = this;
            axios
                .get('/data/project-keys.json')
                .then(function(response) {
                    self.projectKeys = response.data
                })
                .catch(function(e) {
                    console.log(e);
                });
        }
    }
</script>