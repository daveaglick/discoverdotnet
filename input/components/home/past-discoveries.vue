<template>
    <b-card no-body header-bg-variant="secondary" bg-variant="light" class="mb-3 d-none d-md-block">
        <div slot="header">
            <h3>
                <span class="accent-secondary">Recent</span>
                <span class="accent-primary">Discoveries</span>
            </h3>         
        </div>
        <b-card-body>            
            <div v-for="cardData in cardDatas" :key="cardData" class="mb-4">
                <h5 class="text-muted"><i class="far fa-calendar-alt"></i> {{ moment(cardData.discoveryDate).format("LL") }}</h5>
                <component :is="getCard(cardData)" :card-data="cardData">
                    <b-card-body class="flex-0" v-if="cardData.comment">
                        <b-alert show class="mb-0">{{ cardData.comment }}</b-alert>
                    </b-card-body>
                </component>
            </div>
        </b-card-body>
    </b-card>
</template>

<script>
    module.exports = {
        data: function() {
            return {
                cardDatas: {}
            }
        },
        created: function() {
            var self = this;
            axios
                .get('/data/past-discoveries.json')
                .then(function(response) {
                    self.cardDatas = response.data
                })
                .catch(function(e) {
                    console.log(e);
                });
        }
    }
</script>