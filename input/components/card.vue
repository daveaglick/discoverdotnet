<template>
    <b-card no-body header-text-variant="white" header-bg-variant="dark">
        <div slot="header" style="height: 62px">
            <div v-if="icon" class="card-corner card-corner-light"><i class="fa" :class="icon"></i></div>           
            <h5 v-if="cardData.link" class="mb-1 font-weight-bold"><a :href="cardData.link" class="text-light">{{ cardData.title }}</a></h5>
            <h5 v-else class="mb-1 font-weight-bold">{{ cardData.title }}</h5>
            <div v-if="cardData.website" class="small"><a :href="cardData.website" class="text-light">{{ cardData.website | no-protocol }}</a></div>
        </div>
        <slot></slot>
        <b-card-body class="pb-4">        
            <p class="card-text">{{ cardData.description }}</p>
        </b-card-body>      
        <div slot="footer">
            <slot name="footer"></slot>
            <div v-if="cardData.link" class="text-right mt-3">
                <a class="font-weight-bold" :href="cardData.link">More Details  <span class="fa fa-long-arrow-alt-right"></span></a>
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