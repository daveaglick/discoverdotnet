<template>
    <div class="mb-5">
        <div class="text-center">
            <h1>
                <span class="accent-primary">Discovery</span>
                <span class="accent-secondary"><small>of the</small> Day</span>
            </h1>
            <hr />
            <p class="accent-script">A new discovery (almost) every day.</p>
            <h5>{{ cardData.discoveryDate }}</h5>
        </div>
        <component :is="getCard(cardData)" :card-data="cardData">
            <b-card-body class="flex-0" v-if="cardData.comment">
                <b-alert show class="mb-0">{{ cardData.comment }}</b-alert>
            </b-card-body>
        </component> 
    </div>
</template>

<script>
    module.exports = {
        data: function() {
            return {
                cardData: {}
            }
        },
        created: function() {
            axios
                .get('/data/discovery.json')
                .then(response => {
                    this.cardData = response.data
                })
                .catch(e => {
                    console.log(e);
                });
        }
    }
</script>
