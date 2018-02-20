<template>
    <card :card-data="$props.cardData" icon="fa-comments">   
        <slot></slot>   
        <p class="card-text" slot="description">
            {{ cardData.location }}
        </p>        
        <div slot="footer" class="text-right">
            <a class="font-weight-bold" :href="cardData.website">More Details  <span class="fa fa-long-arrow-alt-right"></span></a>
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