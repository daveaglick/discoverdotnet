<template>
    <small-card icon="fa-bug">
        <div class="font-weight-bold"><a :href="cardData.link">{{ cardData.title }}</a></div>        
        <div v-if="project"><a :href="project.link">{{ project.title }}</a></div>
        <div v-if="cardData.labels">
            <b-badge v-for="label in cardData.labels" :key="label" variant="light" class="mr-2">{{ label }}</b-badge>
        </div>
        <slot></slot>
        <div slot="footer">
            <div v-if="cardData.createdAt" class="small">Created {{ cardData.createdAt | from-now }}</div>
            <div v-if="cardData.updatedAt" class="small">Updated {{ cardData.updatedAt | from-now }}</div>
        </div>
    </small-card>
</template>

<script>
    module.exports = {
        props: [
            'cardData',
            'projectKeys'
        ],
        data: function() {
            return {
                project: null
            };
        },
        created: function() {
            var self = this;
            if(this.projectKeys) {
                this.project = this.projectKeys.find(function(item) {
                    return item.key == self.cardData.projectKey;
                });
            }
        }
    }
</script>