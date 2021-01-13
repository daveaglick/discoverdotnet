<template>
    <b-row>
        <b-col class="font-weight-bold">{{ term }}</b-col>
        <b-col>
            <slot></slot>
        </b-col>
    </b-row>
</template>

<script>
    module.exports = {
        props: [
            'term'
        ]
    }
</script>