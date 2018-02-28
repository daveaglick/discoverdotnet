<template>
    <card :card-data="$props.cardData" icon="fa-link">   
        <slot></slot>   
        <div class="card-text" slot="description">              
            <p v-if="cardData.description" class="card-text">
                {{ cardData.description }}
            </p>       
        </div>
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