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
        <dl slot="footer" class="small row">
            <dt v-if="cardData.language" class="col-sm-6">Language</dt>
            <dd v-if="cardData.language" class="col-sm-6"><a href="#"><b-badge>{{ cardData.language }}</b-badge></a></dd>
            <dt v-if="cardData.tags" class="col-sm-6">Tag(s)</dt>
            <dd v-if="cardData.tags" class="col-sm-6">
                <span v-for="tag in cardData.tags" :key="tag">
                    <a href="#"><b-badge>{{ tag }}</b-badge></a>
                </span>
            </dd>
        </dl>
    </card>
</template>

<script>
module.exports = {
    props: [
        'cardData'
    ]
}
</script>