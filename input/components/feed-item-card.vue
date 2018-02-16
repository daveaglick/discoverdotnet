<template>
    <small-card :icon="icon">
        <h5 class="card-title font-weight-bold"><a :href="cardData.link">{{ cardData.title }}</a></h5>      
        <h6 v-if="cardData.published" class="card-subtitle mb-2 text-muted">{{ moment(cardData.published).format("LLLL") }}</h6>
        <div v-if="feed"><a :href="feed.link">{{ feed.title }}</a></div>
        <p v-if="cardData.description" v-html="cardData.description"></p>
        <slot></slot>
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
            };
        },
        created: function() {
            var self = this;
            if(this.feedKeys) {
                this.feed = this.feedKeys.find(function(item) {
                    return item.key == self.cardData.feedKey;
                });
            }
        }
    }
</script>