<template>
    <card :card-data="$props.cardData" icon="fa-comments">   
        <slot></slot>   
        <p class="card-text" slot="description">
            {{ cardData.location }}
        </p>        
        <b-card-body v-if="cardData.nextEventTitle" class="pb-4" slot="bottom">
            <div><small>Next Event</small></div>
            <div class="font-weight-bold"><a :href="cardData.nextEventWebsite">{{ cardData.nextEventTitle }}</a></div>
            <description term="Date">{{ cardData.nextEventDate | event-date }}</description>
            <description term="Time">{{ cardData.nextEventTime | event-time }}</description>
        </b-card-body>
        <div slot="footer" class="text-right">
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