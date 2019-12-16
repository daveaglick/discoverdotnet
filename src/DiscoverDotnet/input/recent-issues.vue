<template>
    <b-card no-body header-bg-variant="secondary" bg-variant="light" class="mb-3">
        <div slot="header">
            <h3>
                <span class="accent-secondary">Recent</span>
                <span class="accent-primary">Issues</span>
            </h3>              
            <hr/>
            <div class="accent-script">Some issues from the last 24 hours. <a href="javascript:void(0)" @click="shuffle">Shuffle</a> to see more.</div>
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
                <p><a href="/issues">All Recent Issues <span class="fa fa-long-arrow-alt-right"></span></a></p>         
                <p><a href="/issues/?tab=netplatform">All .NET Platform Issues <span class="fa fa-long-arrow-alt-right"></span></a></p>         
                <p><a href="/issues/?tab=microsoft">All Microsoft Issues <span class="fa fa-long-arrow-alt-right"></span></a></p>         
                <p><a href="/issues/?tab=netfoundation">All .NET Foundation Issues <span class="fa fa-long-arrow-alt-right"></span></a></p>
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
                .get('/data/issues/recent/all.json')
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
            }
        }
    }
</script>