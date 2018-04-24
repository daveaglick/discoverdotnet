<template>
    <card :card-data="$props.cardData" icon="fa-terminal" :icon-images="true">   
        <slot></slot>
        <b-card-body v-if="cardData.stargazersCount" class="flex-0">
            <b-row class="text-center">
                <b-col>
                    <div><i class="fas fa-star"></i></div>
                    <h5>{{ cardData.stargazersCount }}</h5>
                </b-col>
                <b-col>
                    <div><i class="fas fa-code-branch"></i></div>
                    <h5>{{ cardData.forksCount }}</h5>
                </b-col>
                <b-col>
                    <div><i class="fas fa-bug"></i></div>
                    <h5>{{ cardData.openIssuesCount }}</h5>
                </b-col>
            </b-row>
        </b-card-body>    
        <div slot="bottom">
            <div v-if="cardData.donations" class="bg-gradient-danger text-white text-center p-2">
                <i class="fas fa-heart"></i> <b-link :href="cardData.donations" class="text-white">Donations Accepted</b-link>
            </div>
            <div v-if="issueCounts && issueCounts.helpWantedIssuesCount > 0" class="bg-gradient-success text-white text-center p-2">
                <b-link :href="cardData.link + '?tab=issues&filter=helpwanted'" class="text-white"><b-badge variant="light"><i class="fas fa-bug"></i> {{ issueCounts.helpWantedIssuesCount }}</b-badge> Help Wanted</b-link>
            </div>
        </div> 
        <div slot="footer" class="small">
            <description v-if="cardData.pushedAt" term="Pushed At">
                {{ moment(cardData.pushedAt).format("l LT") }}
            </description>
            <description v-if="cardData.language" term="Language">{{ cardData.language }}</description>
            <description v-if="cardData.tags" term="Tags">
                <div v-for="tag in cardData.tags" :key="tag">{{ tag }}</div>
            </description>
            <div>
                <b-badge v-if="cardData.platform" variant="dark">.NET Platform</b-badge>
                <b-badge v-if="cardData.microsoft" variant="success">Microsoft</b-badge>
                <b-badge v-if="cardData.foundation" variant="primary">.NET Foundation</b-badge>
            </div>
        </div>
    </card>
</template>

<script>
    module.exports = {
        props: [
            'cardData',
            'extraData'
        ],
        data: function() {
            return {
                issueCounts: null
            }
        },
        created: function() {
            var self = this;
            if(this.extraData) {
                this.issueCounts = this.extraData.find(function(item) {
                    return item.projectKey == self.cardData.key;
                })
            }
        }
    }
</script>