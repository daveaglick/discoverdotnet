<template>
    <div class="mb-5 d-sm-none d-md-block">
        <div class="text-center">
            <h3>
                <span class="accent-secondary">Recent</span>
                <span class="accent-primary">Discoveries</span>
            </h3>
            <hr />
        </div>
        <div class="row">
            <div v-for="cardData in cardDatas" :key="cardData" class="col-sm-6 col-md-6 col-lg-6 mb-5 full-height-card">
                <h6 class="text-center">{{ cardData.discoveryDate }}</h6>
                <component :is="getCard(cardData)" :card-data="cardData">
                    <b-card-body class="flex-0" v-if="cardData.comment">
                        <b-alert show class="mb-0">{{ cardData.comment }}</b-alert>
                    </b-card-body>
                </component>
            </div>
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