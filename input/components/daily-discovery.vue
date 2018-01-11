<template>
    <div>
        <div class="text-center">
            <h1><span class="accent-primary">Discovery</span> <span class="accent-script small">of the</span> <span class="accent-secondary">day</span></h1>
            <hr />
            <h5 class="accent-script">{{ discovery.date }}</h5>
            <p>A new featured .NET open source project (almost) every day. Add the <a href="/feed.rss">RSS feed</a> to your feed reader to collect them all.</p>
        </div>
        <project-card :data="discovery"></project-card>
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
