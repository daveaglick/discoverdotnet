<template>
    <b-card no-body header-bg-variant="secondary" bg-variant="light" class="mb-3">
        <div slot="header">
            <h3>
                <span class="accent-primary">Help</span>
                <span class="accent-secondary">Wanted</span>
            </h3>              
            <hr/>
            <div class="accent-script">A few help wanted issues perfect for digging in. <a href="javascript:void(0)" @click="shuffle">Shuffle</a> to see more.</div>
        </div>
        <b-card-body>
            <loading :data="issues"></loading>
            <issue-card v-for="issue in firstIssues" :key="issue.link" :card-data="issue" :project-keys="projectKeys" class="mb-4">
            </issue-card>
        </b-card-body>        
        <div slot="footer" class="text-muted small">
            <div class="float-md-right">
                <b-button size="sm" @click="shuffle">Shuffle</b-button>
            </div>            
            <div class="float-md-left">
                <p><a href="/issues/?tab=helpwanted">All Help Wanted Issues <span class="fa fa-long-arrow-alt-right"></span></a></p>
            </div>
        </div>
    </b-card>
</template>

<script>
    module.exports = {
        props: [
            "projectKeys"
        ],
        data: function() {
            return {
                issues: []
            }
        },
        created: function() {
            var self = this;
            axios
                .get('/data/issues/help-wanted/all.json')
                .then(function(response) {
                    self.issues = response.data
                })
                .catch(function(e) {
                    console.log(e);
                });
        },
        computed: {
            firstIssues: function() {
                return this.issues.slice(0, 4);
            }
        },
        methods: {
            shuffle: function() {
                this.issues = this.issues.sort(function(){return 0.5 - Math.random()});
            },
            getProject: function(issue) {
                return this.projectKeys.find(function(item) {
                    return item.key == issue.projectKey;
                });
            }
        }
    }
</script>