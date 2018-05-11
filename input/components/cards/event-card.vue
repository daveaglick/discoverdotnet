<template>
    <card :card-data="$props.cardData" icon="fa-calendar-alt">   
        <slot></slot>   
        <div class="card-text" slot="description">              
            <p v-if="cardData.description" class="card-text">
                {{ cardData.description }}
            </p>       
            <div v-if="cardData.date">
                <i class="far fa-calendar-alt"></i> 
                <span class="font-weight-bold">{{ cardData.date | event-date }}</span><span v-if="cardData.time"> {{ cardData.time | event-time }}</span>
                <span v-if="cardData.endDate">
                    to <span class="font-weight-bold">{{ cardData.endDate | event-date }}</span><span v-if="cardData.endTime"> {{ cardData.endTime | event-time }}</span>
                </span>
            </div>
        </div>
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