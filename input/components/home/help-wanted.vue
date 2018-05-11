<template>
    <div class="mb-5">
        <div class="text-center mb-5">            
            <h3>
                <span class="accent-primary">Help</span>
                <span class="accent-secondary">Wanted</span>
                
                <!-- This adds extra height to line up the hr -->
                <span class="h1"></span>
            </h3>
            <hr/>
            <p class="accent-script">A few help wanted issues perfect for digging in. <a href="javascript:void(0)" @click="shuffle">Shuffle</a> to see more.</p>       

            <loading :data="issues"></loading>

            <issue-card v-for="issue in firstIssues" :key="issue.link" :card-data="issue" :project-keys="projectKeys" class="mb-4">
            </issue-card>
            
            <div class="text-right mb-4"><b-button size="sm" class="mr-sm-2 mt-2 mt-sm-0" @click="shuffle">Shuffle</b-button></div>            
            <p class="text-left"><a href="/issues/?tab=helpwanted">All Help Wanted Issues <span class="fa fa-long-arrow-alt-right"></span></a></p>
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
            axios
                .get('/data/issues/help-wanted/all.json')
                .then(response => {
                    this.issues = response.data
                })
                .catch(e => {
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