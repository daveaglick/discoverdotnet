<template>
    <b-card no-body header-text-variant="white" header-bg-variant="dark" footer-bg-variant="white" footer-border-variant="white">
        <div slot="header" style="height: 56px">
            <div v-if="icon" class="card-corner card-corner-light"><i class="fa" :class="icon"></i></div>           
            <h4 v-if="cardData.link" class="mb-1"><a :href="cardData.link" class="text-light">{{ cardData.title }}</a></h4>
            <h4 v-else class="mb-1">{{ cardData.title }}</h4>
            <h6 v-if="cardData.website"><a :href="cardData.website" class="text-light">{{ cardData.website | no-protocol }}</a></h6>
        </div>
        <slot></slot>
        <b-card-body class="pb-1">        
            <p class="card-text">{{ cardData.description }}</p>
        </b-card-body>      
        <div slot="footer">
            <slot name="footer"></slot>
            <div v-if="cardData.link" class="text-right mt-3">
                <b-button :href="cardData.link">More Details</b-button>
            </div>
        </div>
    </b-card>
</template>

<script>
module.exports = {
    props: [
        'cardData',
        'icon'
    ]
}
</script>