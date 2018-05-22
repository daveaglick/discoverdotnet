<template>
    <div class="mb-5">
        <div class="text-center">
            <h1>
                <span class="accent-primary">Discovery</span>
                <span class="accent-secondary"><small>of the</small> Day</span>
            </h1>
            <hr />
            <p class="accent-script">A new discovery (almost) every day.</p>
            <h5 class="text-muted"><i class="far fa-calendar-alt"></i> {{ moment(cardData.discoveryDate).format("LL") }}</h5>
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
            var self = this;
            axios
                .get('/data/discovery.json')
                .then(function(response) {
                    self.cardData = response.data
                })
                .catch(function(e) {
                    console.log(e);
                });
        }
    }
</script>
