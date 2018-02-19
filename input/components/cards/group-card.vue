<template>
    <card :card-data="$props.cardData" icon="fa-comments">   
        <slot></slot>   
        <p class="card-text" slot="description">
            {{ cardData.location }}
        </p>
    </card>
</template>

<script>
    module.exports = {
        props: [
            'cardData'
        ]
    }
</script>