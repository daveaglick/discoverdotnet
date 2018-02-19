<template>
    <card :card-data="$props.cardData" icon="fa-newspaper">   
        <slot></slot>   
        <div slot="footer" class="small">
            <description v-if="cardData.lastPublished" term="Last Published">
                {{ cardData.lastPublished | from-now }}
            </description>
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