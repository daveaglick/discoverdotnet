<template>
    <div v-if="results !== null && results.nbHits > 0">
        <h5 class="text-muted mt-2">{{ title }}</h5>
        <div v-for="result in results.hits" :key="result.objectID" class="mt-1">
            <slot :result="result"></slot>
        </div>
    </div>
</template>

<script>
    module.exports = {
        props: {
            title: null,
            results: null
        }
    }
</script>