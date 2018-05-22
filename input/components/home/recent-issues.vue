<template>
    <div class="mb-5">
        <div class="text-center mb-5">            
            <h3>
                <span class="accent-secondary">Recent</span>
                <span class="accent-primary">Issues</span>
                
                <!-- This adds extra height to line up the hr -->
                <span class="h1"></span>
            </h3>
            <hr/>
            <p class="accent-script">Some issues from the last 24 hours. <a href="javascript:void(0)" @click="shuffle">Shuffle</a> to see more.</p>        
            
            <loading :data="issues"></loading>

            <issue-card v-for="issue in firstIssues" :key="issue.link" :card-data="issue" :project-keys="projectKeys" class="mb-4">
            </issue-card>
        
            <div class="text-right mb-4"><b-button size="sm" class="mr-sm-2 mt-2 mt-sm-0" @click="shuffle">Shuffle</b-button></div>                     
            <p class="text-left"><a href="/issues">All Recent Issues <span class="fa fa-long-arrow-alt-right"></span></a></p>         
            <p class="text-left"><a href="/issues/?tab=netplatform">All .NET Platform Issues <span class="fa fa-long-arrow-alt-right"></span></a></p>         
            <p class="text-left"><a href="/issues/?tab=microsoft">All Microsoft Issues <span class="fa fa-long-arrow-alt-right"></span></a></p>         
            <p class="text-left"><a href="/issues/?tab=netfoundation">All .NET Foundation Issues <span class="fa fa-long-arrow-alt-right"></span></a></p>
        </div>
    </div>
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