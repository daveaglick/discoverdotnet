<template>
    <card :card-data="$props.cardData" icon="fa-comments">   
        <slot></slot>    
        <b-card-body v-if="cardData.nextEventTitle" class="pb-4" slot="bottom">
            <div><small>Next Event</small></div>
            <div class="font-weight-bold"><a :href="cardData.nextEventWebsite">{{ cardData.nextEventTitle }}</a></div>
            <div><small>{{ cardData.nextEventDate | event-date }} {{ cardData.nextEventTime | event-time }}</small></div>
        </b-card-body>
        <div slot="footer" class="text-right">
            <div v-if="cardData.location">{{ cardData.location }}</div>
            <a class="font-weight-bold" :href="cardData.website">More Details  <span class="fa fa-long-arrow-alt-right"></span></a>
        </div>
    </card>
</template>

<script>
    module.exports = {
        props: [
            'cardData'
        ]
    }
</script>