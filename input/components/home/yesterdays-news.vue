<template>
    <div class="mb-5">
        <div class="text-center">            
            <h3>
                <span class="accent-secondary">Yesterday's</span>
                <span class="accent-primary">News</span>
                
                <!-- This adds extra height to line up the hr -->
                <span class="h1"></span>
            </h3>
            <hr/>
            <p class="accent-script">A roundup of recent blog posts, podcasts, and more.</p>
        </div>
        
        <loading :data="cardData"></loading>

        <div class="row">
            <div v-for="card in cardData" :key="card.link" class="col-sm-12 col-md-6 col-lg-6 col-xl-4 mb-4 full-height-card">
                <feed-item-card :icon="getIcon(card)" :card-data="card">
                </feed-item-card>
            </div>
        </div>

        <div class="text-center">
            <b-alert show variant="secondary" class="d-inline-block">
                <i class="fas fa-rss"></i> <a href="/feeds/news.atom" class="mr-2">Atom</a>
                <i class="fas fa-rss"></i> <a href="/feeds/news.rss">RSS</a>
            </b-alert>
        </div>
        <p><a href="/blogs">More Blogs <span class="fa fa-long-arrow-alt-right"></span></a></p>
        <p><a href="/broadcasts">More Broadcasts <span class="fa fa-long-arrow-alt-right"></span></a></p>
    </div>
</template>


<script>
    module.exports = {
        data: function() {
            return {
                cardData: []
            }
        },
        created: function() {
            axios
                .get('/data/news.json')
                .then(response => {
                    this.cardData = response.data
                })
                .catch(e => {
                    console.log(e);
                });
        },
        methods: {
            getIcon: function(card) {
                if(card.feedLink.startsWith('/blogs/')) {
                    return "fa-pencil-alt";
                }
                if(card.feedLink.startsWith('/broadcasts/')) {
                    return "fa-headphones";
                }
                return "";
            }
        }
    }
</script>