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

        <feed-item-card v-for="card in cardData" :key="card.link" :icon="getIcon(card)" :card-data="card" :feed-keys="feedKeys">
        </feed-item-card>

        <p><a href="/blogs">More Blogs <span class="fa fa-long-arrow-alt-right"></span></a></p>
        <p><a href="/broadcasts">More Broadcasts <span class="fa fa-long-arrow-alt-right"></span></a></p>
    </div>
</template>


<script>
    module.exports = {
        data: function() {
            return {
                cardData: [],
                feedKeys: []
            }
        },
        created: function() {
            axios
                .get('/data/recent-feed-items.json')
                .then(response => {
                    this.cardData = response.data
                })
                .catch(e => {
                    console.log(e);
                });
            axios
                .get('/data/feed-keys.json')
                .then(response => {
                    this.feedKeys = response.data
                })
                .catch(e => {
                    console.log(e);
                });
        },
        methods: {
            getIcon: function(card) {
                if(card.feedKey.startsWith('blogs/')) {
                    return "fa-pencil-alt";
                }
                if(card.feedKey.startsWith('broadcasts/')) {
                    return "fa-headphones";
                }
                return "";
            }
        }
    }
</script>