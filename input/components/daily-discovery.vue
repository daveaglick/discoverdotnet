<template>
    <div>
        <div class="text-center">
            <h1><span class="accent-primary">Discovery</span> <span class="accent-script small">of the</span> <span class="accent-secondary">day</span></h1>
            <hr />
            <h5 class="accent-script">{{ discovery.date }}</h5>
            <p class="small">A new discovery (almost) every day.<br />Add the <a href="/feed.rss">RSS feed</a> to your feed reader to collect them all.</p>
        </div>
        <b-row>
            <b-col>Short writeup of the daily discovery goes here.</b-col>
            <b-col><component :is="getCard(discovery)" :card-data="discovery"></component></b-col>
        </b-row>        
    </div>
</template>

<script>
 module.exports = {
    data: function() {
        return {
            discovery: {}
        }
    },
    created: function() {
        axios
            .get('/data/daily-discovery.json')
            .then(response => {
                this.discovery = response.data
            })
            .catch(e => {
                console.log(e);
            });
    }
}
</script>
