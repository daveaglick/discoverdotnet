<template>
    <div>
        <loading :data="cardData"></loading>

        <feed-item-card v-for="card in cardData" :key="card.link" :icon="icon" :card-data="card" class="mb-4">
        </feed-item-card>
    </div>
</template>

<script>
    module.exports = {
        props: [
            'feedItemJson',
            'icon'
        ],
        data: function() {
            return {
                cardData: []
            }
        },
        created: function() {
            axios
                .get(this.feedItemJson)
                .then(response => {
                    this.cardData = response.data
                })
                .catch(e => {
                    console.log(e);
                });
        }
    }
</script>