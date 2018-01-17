<template>
    <card :card-data="$props.cardData" icon="fa-terminal">   
        <slot></slot>
        <b-card-body v-if="cardData.stargazersCount" class="flex-0">
            <b-row class="text-center">
                <b-col>
                    <div><i class="fas fa-star"></i></div>
                    <h4>{{ cardData.stargazersCount }}</h4>
                </b-col>
                <b-col>
                    <div><i class="fas fa-code-branch"></i></div>
                    <h4>{{ cardData.forksCount }}</h4>
                </b-col>
                <b-col>
                    <div><i class="fas fa-bug"></i></div>
                    <h4>{{ cardData.openIssuesCount }}</h4>
                </b-col>
            </b-row>
        </b-card-body>     
        <dl slot="footer" class="small row">
            <dt class="col-sm-6">Language</dt>
            <dd class="col-sm-6"><a href="#"><b-badge>C#</b-badge></a></dd>
            <dt class="col-sm-6">Tag(s)</dt>
            <dd class="col-sm-6"><a href="#"><b-badge>static sites</b-badge></a></dd>
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