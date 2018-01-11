<template>
    <card :data="$props.data">
        <b-card-body v-if="data.stars" class="flex-0">
            <b-row class="text-center">
                <b-col>
                    <div><i class="fas fa-star"></i></div>
                    <h4>{{ data.stars }}</h4>
                </b-col>
                <b-col>
                    <div><i class="fas fa-code-branch"></i></div>
                    <h4>{{ data.forkCount }}</h4>
                </b-col>
                <b-col>
                    <div><i class="fas fa-bug"></i></div>
                    <h4>{{ data.issueCount }}</h4>
                </b-col>
            </b-row>
        </b-card-body>
        <dl slot="footer" class="small row">
            <dt v-if="data.source" class="col-sm-6">Source</dt>
            <dd v-if="data.source" class="col-sm-6"><a :href="data.source">{{ data.source | no-protocol }}</a></dd>
            <dt v-if="data.nuget" class="col-sm-6">NuGet</dt>
            <dd v-if="data.nuget" class="col-sm-6"><a :href="'https://www.nuget.org/packages/' + data.nuget">{{ data.nuget }}</a></dd>
            <dt v-if="data.twitter" class="col-sm-6">Twitter</dt>
            <dd v-if="data.twitter" class="col-sm-6"><a :href="'https://twitter.com/' + data.twitter">@{{ data.twitter }}</a></dd>
            <dt v-if="data.chat" class="col-sm-6">Chat</dt>
            <dd v-if="data.chat" class="col-sm-6"><a :href="data.chat">{{ data.chat | no-protocol }}</a></dd>
            <dt class="col-sm-6">Language</dt>
            <dd class="col-sm-6"><a href="#"><b-badge>C#</b-badge></a></dd>
            <dt class="col-sm-6">Tag(s)</dt>
            <dd class="col-sm-6"><a href="#"><b-badge>static sites</b-badge></a></dd>
        </dl>
    </card>
</template>

<script>
module.exports = {
    props: [
        'data'
    ]
}
</script>