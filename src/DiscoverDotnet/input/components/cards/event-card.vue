<template>
    <card :card-data="$props.cardData" icon="fa-calendar-alt">   
        <slot>            
            <b-card-body v-if="cardData.date" class="flex-0">
                <i class="far fa-calendar-alt"></i> 
                <span class="font-weight-bold">{{ cardData.date | event-date }}</span><span v-if="cardData.time"> {{ cardData.time | event-time }}</span>
                <span v-if="cardData.endDate">
                    to <span class="font-weight-bold">{{ cardData.endDate | event-date }}</span><span v-if="cardData.endTime"> {{ cardData.endTime | event-time }}</span>
                </span>
            </b-card-body>
        </slot>   
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