<template>
    <span v-if="filterValues.length > 0">
        <span class="h6 text-muted mx-2">{{ text }}</span>
        <b-button
            v-for="filterValue in filterValues"
            :key="filterValue"
            v-if="filterValue != null && filterValue.length > 0"
            @click="filterSelected(filterValue)"
            size="sm"
            class="mb-2 mr-2"            
            :variant="isSelected(filterValue) ? 'primary' : 'light'">
            {{ filterValue }}
        </b-button>
    </span>
</template>

<script>
    module.exports = {
        props: [
            'text',
            'values',
            'selected'
        ],
        computed: {
            // This was helpful: https://stackoverflow.com/a/40691525/807064
            filterValues: function() {
                return this.values.reduce(function(accumulator, item) {
                    // Check if this is an array value
                    if(Array.isArray(item)) {
                        // It's an array, filter array items not in the accumulator and then add them
                        accumulator = accumulator.concat(item.filter(function(item) {
                            return accumulator.indexOf(item) === -1;
                        }));
                    } else if(accumulator.indexOf(item) === -1) {
                        // Not an array, add the item if it's not already in the accumulator
                        accumulator.push(item);
                    }
                    return accumulator;
                }, []).sort();
            }
        },
        methods: {
            isSelected: function(filterValue) {
                return this.selected.indexOf(filterValue) !== -1;
            },
            filterSelected: function(filterValue) {
                var index = this.selected.indexOf(filterValue);
                if(index === -1) {
                    this.selected.push(filterValue);
                } else {
                    this.selected.splice(index, 1);
                }
            }
        }
    }
</script>