<template>
    <b-card no-body header-bg-variant="primary" header-text-variant="white" bg-variant="light" class="mb-3">
        <div slot="header">
            <h2>
                <span class="accent-secondary">Recent</span>
                <span class="accent-primary">News</span>
            </h2>
            <hr/>
            <div class="accent-script">A roundup of recent blog posts, podcasts, and more.</div>       
        </div>
        <b-card-body>
            <loading :data="cardData"></loading>
            <div class="row">
                <div v-for="card in cardData" :key="card.link" class="col-sm-12 col-md-6 col-lg-6 col-xl-4 mb-4 full-height-card">
                    <feed-item-card :icon="getIcon(card)" :card-data="card">
                    </feed-item-card>
                </div>
            </div>
        </b-card-body>
        <div slot="footer" class="text-muted small">
            <div>
                <span class="font-weight-bold mr-2">Recent news feed: </span>
                <i class="fas fa-rss"></i> <a href="/feeds/news.atom" class="mr-2">Atom</a>
                <i class="fas fa-rss"></i> <a href="/feeds/news.rss" class="mr-2">RSS</a>
                <i class="fas fa-rss"></i> <a href="/feeds.opml">OPML</a>
            </div>
            <hr />
            <div><a href="/blogs">More Blogs <span class="fa fa-long-arrow-alt-right"></span></a></div>
            <div><a href="/broadcasts">More Broadcasts <span class="fa fa-long-arrow-alt-right"></span></a></div>
        </div>
    </b-card>
</template>


<script>
    module.exports = {
        data: function() {
            return {
                cardData: []
            }
        },
        created: function() {
            var self = this;
            axios
                .get('/data/news.json')
                .then(function(response) {
                    self.cardData = response.data
                })
                .catch(function(e) {
                    console.log(e);
                });
        },
        methods: {
            getIcon: function(card) {
                if(card.feedLink.startsWith('/blogs/')) {
                    return "fa-align-left";
                }
                if(card.feedLink.startsWith('/broadcasts/')) {
                    return "fa-headphones";
                }
                return "";
            }
        }
    }
</script>