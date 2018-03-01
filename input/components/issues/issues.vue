<template>
    <div>
        <b-tabs pills v-model="tabIndex">
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
        </b-tabs>
    </div>
</template>


<script>
    module.exports = {
        data: function() {
            return {
                projectKeys: [],
                totalCounts: {},
                tabIndex: 0
            }
        },
        created: function() {
            // Go to a specific tab
            var tab = location.search.substr(1).split("&")[0];
            if(tab === "help-wanted") {
                this.tabIndex = 1;
            } else if(tab === "platform") {
                this.tabIndex = 2;
            } else if(tab === "microsoft") {
                this.tabIndex = 3;
            } else if(tab === "foundation") {
                this.tabIndex = 4;
            }

            axios
                .get('/data/issues/total-counts.json')
                .then(response => {
                    this.totalCounts = response.data
                })
                .catch(e => {
                    console.log(e);
                });                
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