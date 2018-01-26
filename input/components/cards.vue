<template>
    <div>
        <b-card-group deck v-for="i in Math.ceil(cardData.length / 4)" :key="i" class="mb-4">
            <component v-if="(i - 1) * 4 < cardData.length" :is="getCard(cardData[(i - 1) * 4])" :card-data="cardData[(i - 1) * 4]"></component>
            <b-card v-else class="invisible"></b-card>            
            <component v-if="((i - 1) * 4) + 1 < cardData.length" :is="getCard(cardData[((i - 1) * 4) + 1])" :card-data="cardData[((i - 1) * 4) + 1]"></component>
            <b-card v-else class="invisible"></b-card>            
            <div class="w-100 mb-4 d-lg-none"></div>
            <component v-if="((i - 1) * 4) + 2 < cardData.length" :is="getCard(cardData[((i - 1) * 4) + 2])" :card-data="cardData[((i - 1) * 4) + 2]"></component>
            <b-card v-else class="invisible"></b-card>            
            <component v-if="((i - 1) * 4) + 3 < cardData.length" :is="getCard(cardData[((i - 1) * 4) + 3])" :card-data="cardData[((i - 1) * 4) + 3]"></component>
            <b-card v-else class="invisible"></b-card>            
        </b-card-group>
    </div>
</template>

<script>
    module.exports = {
        props: [
            'cardData'
        ]
    }
</script>