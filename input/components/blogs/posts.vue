<template>
    <div>
        <post-card v-for="card in cardData" :key="card.link" icon="fa-pencil-alt" :card-data="card" :source-keys="sourceKeys">
        </post-card>
    </div>
</template>

<script>
    module.exports = {
        props: [
            'postJson',
            'sourceKeys',
        ],
        data: function() {
            return {
                cardData: []
            }
        },
        created: function() {
            axios
                .get(this.postJson)
                .then(response => {
                    this.cardData = response.data
                })
                .catch(e => {
                    console.log(e);
                });
        }
    }
</script>