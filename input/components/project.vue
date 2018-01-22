<template>
    <card :card-data="$props.cardData" icon="fa-terminal">   
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
        <div slot="footer" class="small">
            <description v-if="cardData.pushedAt" term="Pushed At">
                {{ moment(cardData.pushedAt).format("l LT") }}
            </description>
            <description v-if="cardData.language" term="Language">{{ cardData.language }}</description>
            <description v-if="cardData.tags" term="Tags">
                <div v-for="tag in cardData.tags" :key="tag">{{ tag }}</div>
            </description>
        </div>
    </card>
</template>

<script>
module.exports = {
    props: [
        'cardData'
    ]
}
</script>