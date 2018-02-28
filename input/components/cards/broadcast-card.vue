<template>
    <card :card-data="$props.cardData" icon="fa-rss">   
        <slot></slot>          
        <b-card-body v-if="cardData.newestFeedItemTitle" class="pb-4" slot="bottom">
            <div><small>Most Recent</small></div>
            <div class="font-weight-bold"><a :href="cardData.newestFeedItemLink">{{ cardData.newestFeedItemTitle }}</a></div>
            <div><small>{{ moment(cardData.newestFeedItemPublished).format("LL") }}</small></div>
        </b-card-body>
        <div slot="footer" class="small">
            <description v-if="cardData.lastPublished" term="Last Published">
                {{ cardData.lastPublished | from-now }}
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