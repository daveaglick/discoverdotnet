<template>
    <b-card no-body class="text-left">
        <div v-if="icon" class="card-corner card-corner-dark"><i class="fa" :class="icon"></i></div>
        <b-card-body class="pb-3">
            <slot></slot>
        </b-card-body>
        <div slot="footer">
            <slot name="footer"></slot>
        </div>
    </b-card>
</template>

<script>
    module.exports = {
        props: [
            'icon'
        ]
    }
</script>