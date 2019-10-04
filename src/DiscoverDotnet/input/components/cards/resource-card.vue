<template>
    <card :card-data="$props.cardData" icon="fa-link">   
        <slot></slot>   
        <div class="card-text" slot="description">              
            <p v-if="cardData.description" class="card-text">
                {{ cardData.description }}
            </p>       
        </div>        
        <div slot="footer">
            <div class="small">
                <description v-if="cardData.tags" term="Tags">
                    <div v-for="tag in cardData.tags" :key="tag"><a :href="'/resources/?filter-tags=' + tag"><i class="fas fa-tag"></i> {{ tag }}</a></div>
                </description>
            </div>
            <div class="text-right mt-3">
                <a class="font-weight-bold" :href="cardData.website">More Details  <span class="fa fa-long-arrow-alt-right"></span></a>
            </div>
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