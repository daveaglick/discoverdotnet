<template>
    <small-card :icon="icon">
        <h4 class="card-title"><a :href="cardData.link">{{ cardData.title }}</a></h4>      
        <h6 v-if="cardData.published" class="card-subtitle mb-2 text-muted">{{ moment(cardData.published).format("LLLL") }}</h6>
        <div v-if="sourceKeys"><a :href="getSource(cardData).link">{{ getSource(cardData).title }}</a></div>
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
            'sourceKeys'
        ],
        methods: {
            getSource: function(post) {
                return this.sourceKeys.find(function(item) {
                    return item.key == post.sourceKey;
                });
            }
        }
    }
</script>