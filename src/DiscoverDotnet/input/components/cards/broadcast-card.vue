<template>
    <card :card-data="$props.cardData" icon="fa-rss">   
        <slot></slot>          
        <b-card-body v-if="cardData.newestFeedItem" class="pb-4 flex-0" slot="bottom">
            <div><small>Most Recent</small></div>
            <div class="font-weight-bold"><a :href="cardData.newestFeedItem.link">{{ cardData.newestFeedItem.title }}</a></div>
            <play-button :card-data="cardData.newestFeedItem"></play-button>
            <div><small>{{ moment(cardData.newestFeedItem.published).format("LL") }}</small></div>
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