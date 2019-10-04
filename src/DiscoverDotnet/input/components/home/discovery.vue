<template>
    <b-card no-body header-bg-variant="primary" header-text-variant="white" bg-variant="light" class="mb-3">
        <div slot="header">
            <h1>
                <span class="accent-primary">Discovery</span>
                <span class="accent-secondary"><small>of the</small> Day</span>
            </h1>
            <hr />
            <div class="accent-script">A new discovery (almost) every day.</div>            
        </div>
        <b-card-body>            
            <h5 class="text-muted"><i class="far fa-calendar-alt"></i> {{ moment(cardData.discoveryDate).format("LL") }}</h5>
            <component :is="getCard(cardData)" :card-data="cardData">
                <b-card-body class="flex-0" v-if="cardData.comment">
                    <b-alert show class="mb-0">{{ cardData.comment }}</b-alert>
                </b-card-body>
            </component> 
        </b-card-body>
        <div slot="footer" class="text-muted small">
            <div>
                <span class="font-weight-bold mr-2">Discovery feed: </span>
                <i class="fas fa-rss"></i> <a href="/feeds/discoveries.atom" class="mr-2">Atom</a>
                <i class="fas fa-rss"></i> <a href="/feeds/discoveries.rss">RSS</a>
            </div>
        </div>
    </b-card>
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
