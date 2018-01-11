<template>
    <b-card no-body header-text-variant="white" header-bg-variant="dark" footer-bg-variant="white" footer-border-variant="white">
        <div slot="header" class="text-center">
            <h4 v-if="data.link"><a :href="data.link" class="text-light">{{ data.title }}</a></h4>
            <h4 v-else>{{ data.title }}</h4>
            <h6 v-if="data.website"><a :href="data.website" class="text-light">{{ data.website | no-protocol }}</a></h6>
        </div>
        <slot></slot>
        <b-card-body>        
            <p class="card-text">{{ data.description }}</p>
        </b-card-body>      
        <div slot="footer">
            <slot name="footer"></slot>
            <div v-if="data.link" class="text-center">
                <b-button size="sm" variant="light" :href="data.link">More Details</b-button>
            </div>
        </div>
    </b-card>
</template>

<script>
module.exports = {
    props: [
        'data'
    ]
}
</script>