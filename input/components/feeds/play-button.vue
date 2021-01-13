<template>
    <div v-if="cardData.links && (cardData.links['audio/mp3'] || cardData.links['audio/mpeg'] || cardData.links['video/mp4'])">
        <b-button v-if="cardData.links && cardData.links['audio/mp3']" variant="primary" size="sm" :href="cardData.links['audio/mp3']" class="mb-2">
            <i class="fa fa-play-circle"></i> Listen
        </b-button>
        <b-button v-else-if="cardData.links && cardData.links['audio/mpeg']" variant="primary" size="sm" :href="cardData.links['audio/mpeg']" class="mb-2">
            <i class="fa fa-play-circle"></i> Listen
        </b-button>
        <b-button v-else-if="cardData.links && cardData.links['video/mp4']" variant="primary" size="sm" :href="cardData.links['video/mp4']" class="mb-2">
            <i class="fa fa-play-circle"></i> Watch
        </b-button>
    </div>
</template>

<script>
    module.exports = {
        props: [
            'cardData'
        ]
    }
</script>