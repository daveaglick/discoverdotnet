<template>
    <div>        
        <div class="text-center mb-4">        
            <h1>
                <span class="accent-secondary">All</span>
                <span class="accent-primary">Issues</span>
            </h1>
        </div>

        <body-tabs>
            <b-tab title="All Issues" class="mt-4">
                <paged-grid
                    card-json="/data/issues/all/"
                    :total-count="totalCounts.all">
                    <issue-card slot-scope="props" :card-data="props.cardData" :project-keys="projectKeys">
                    </issue-card>
                </paged-grid>
            </b-tab>
            <b-tab title="Help Wanted" class="mt-4">
                <paged-grid
                    card-json="/data/issues/help-wanted/"
                    :total-count="totalCounts.helpWanted">
                    <issue-card slot-scope="props" :card-data="props.cardData" :project-keys="projectKeys">
                    </issue-card>
                </paged-grid>
            </b-tab>
            <b-tab title=".NET Platform" class="mt-4">
                <paged-grid
                    card-json="/data/issues/platform/"
                    :total-count="totalCounts.platform">
                    <issue-card slot-scope="props" :card-data="props.cardData" :project-keys="projectKeys">
                    </issue-card>
                </paged-grid>
            </b-tab>
            <b-tab title="Microsoft" class="mt-4">
                <paged-grid
                    card-json="/data/issues/microsoft/"
                    :total-count="totalCounts.microsoft">
                    <issue-card slot-scope="props" :card-data="props.cardData" :project-keys="projectKeys">
                    </issue-card>
                </paged-grid>
            </b-tab>
            <b-tab title=".NET Foundation" class="mt-4">
                <paged-grid
                    card-json="/data/issues/foundation/"
                    :total-count="totalCounts.foundation">
                    <issue-card slot-scope="props" :card-data="props.cardData" :project-keys="projectKeys">
                    </issue-card>
                </paged-grid>
            </b-tab>
        </body-tabs>
    </div>
</template>


<script>
    module.exports = {
        props: [
            'projectKeys'
        ],
        data: function() {
            return {
                totalCounts: {}
            }
        },
        created: function() {
            axios
                .get('/data/issues/total-counts.json')
                .then(response => {
                    this.totalCounts = response.data
                })
                .catch(e => {
                    console.log(e);
                });
        }
    }
</script>