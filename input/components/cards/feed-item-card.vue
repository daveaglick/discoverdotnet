<template>
    <small-card :icon="icon">
        <div v-if="cardData.links && cardData.links.image"><b-img fluid :src="cardData.links.image" class="small-card-image mb-2"></b-img></div>
        <h5 class="card-title font-weight-bold mb-2"><a :href="cardData.link">{{ cardData.title }}</a></h5>
        <div v-if="cardData.author" class="text-muted mb-2">By {{ cardData.author }}</div>
        <h6 v-if="cardData.published" class="card-subtitle mb-2 text-muted">{{ moment(cardData.published).format("LL") }}</h6>
        <play-button :card-data="cardData"></play-button>
        <div v-if="cardData.feedLink"><a :href="cardData.feedLink">{{ cardData.feedTitle }}</a></div>
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
            'cardData'
        ],
        data: function() {
            return {
                collapsed: true
            };
        },
        methods: {
            expand: function() {
                this.collapsed = false;
            }
        }
    }
</script>