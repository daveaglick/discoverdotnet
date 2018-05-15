<template>
    <div class="mb-5 d-none d-md-block">
        <div class="text-center">
            <h3>
                <span class="accent-secondary">Recent</span>
                <span class="accent-primary">Discoveries</span>
            </h3>
            <hr />
        </div>
        <div v-for="cardData in cardDatas" :key="cardData" class="mb-5">
            <div class="text-center">
                <h5 class="text-muted"><i class="far fa-calendar-alt"></i> {{ moment(cardData.discoveryDate).format("LL") }}</h5>
            </div>
            <component :is="getCard(cardData)" :card-data="cardData">
                <b-card-body class="flex-0" v-if="cardData.comment">
                    <b-alert show class="mb-0">{{ cardData.comment }}</b-alert>
                </b-card-body>
            </component>
        </div>
    </div>
</template>

<script>
    module.exports = {
        data: function() {
            return {
                cardDatas: {}
            }
        },
        created: function() {
            axios
                .get('/data/past-discoveries.json')
                .then(response => {
                    this.cardDatas = response.data
                })
                .catch(e => {
                    console.log(e);
                });
        }
    }
</script>