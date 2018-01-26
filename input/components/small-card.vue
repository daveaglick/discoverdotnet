<template>
    <b-card class="mb-4 text-left">
        <div v-if="icon" class="card-corner card-corner-dark"><i class="fa" :class="icon"></i></div>
        <slot></slot>
    </b-card>
</template>

<script>
    module.exports = {
        props: [
            'icon'
        ]
    }
</script>