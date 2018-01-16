<template>
    <div>
        <b-card-group deck v-for="i in Math.ceil(cardsData.length / 4)" :key="i" class="mb-4">
            <component v-if="(i - 1) * 4 < cardsData.length" :is="getCard(cardsData[(i - 1) * 4])" :card-data="cardsData[(i - 1) * 4]"></component>
            <b-card v-else class="invisible"></b-card>            
            <component v-if="((i - 1) * 4) + 1 < cardsData.length" :is="getCard(cardsData[((i - 1) * 4) + 1])" :card-data="cardsData[((i - 1) * 4) + 1]"></component>
            <b-card v-else class="invisible"></b-card>            
            <div class="w-100 mb-4 d-lg-none"></div>
            <component v-if="((i - 1) * 4) + 2 < cardsData.length" :is="getCard(cardsData[((i - 1) * 4) + 2])" :card-data="cardsData[((i - 1) * 4) + 2]"></component>
            <b-card v-else class="invisible"></b-card>            
            <component v-if="((i - 1) * 4) + 3 < cardsData.length" :is="getCard(cardsData[((i - 1) * 4) + 3])" :card-data="cardsData[((i - 1) * 4) + 3]"></component>
            <b-card v-else class="invisible"></b-card>            
        </b-card-group>
    </div>
</template>

<script>
module.exports = {
    props: [
        'cardsData'
    ]
}
</script>