<template>
    <small-card :icon="icon">
        <div v-if="cardData.links && cardData.links.image"><b-img fluid :src="cardData.links.image" class="small-card-image mb-2"></b-img></div>
        <h5 class="card-title font-weight-bold"><a :href="cardData.link">{{ cardData.title }}</a></h5>      
        <h6 v-if="cardData.published" class="card-subtitle mb-2 text-muted">{{ moment(cardData.published).format("LL") }}</h6>
        <b-button v-if="cardData.links && cardData.links['audio/mp3']" variant="primary" size="sm" :href="cardData.links['audio/mp3']" class="mb-2">
            <i class="fa fa-play-circle"></i> Listen
        </b-button>
        <div v-if="feed"><a :href="feed.link">{{ feed.title }}</a></div>
        <slot></slot>
        <div v-if="cardData.description" :class="{ 'collapsed-detail': collapsed }">
            <div v-html="cardData.description"></div>
            <div v-if="collapsed" class="fade-detail text-center"></div>
        </div>     
        <div v-if="cardData.description && collapsed"><a href="javascript:void(0)" @click="expand">Expand...</a></div>            
        <div slot="footer">
            <div v-if="cardData.published" class="small">Published {{ cardData.published | from-now }}</div>
        </div>
    </small-card>
</template>

<script>
    module.exports = {
        props: [
            'icon',
            'cardData',
            'feedKeys'
        ],
        data: function() {
            return {
                feed: null,
                collapsed: true
            };
        },
        created: function() {
            var self = this;
            if(this.feedKeys) {
                this.feed = this.feedKeys.find(function(item) {
                    return item.key == self.cardData.feedKey;
                });
            }
        },
        methods: {
            expand: function() {
                this.collapsed = false;
            }
        }
    }
</script>