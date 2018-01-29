<template>
    <div>
        <card-grid
            :card-json="issueJson"
            :filters="filters"
            :sorts="sorts">
            <div slot-scope="props">
                <ul>
                    <li v-for="card in props.cardData" :key="card.title">{{ card.title }}</li>
                </ul>
            </div>
        </card-grid>
    </div>
</template>


<script>
    module.exports = {
        props: [
            'issueJson'
        ],
        data: function() {
            return {
                filters: [
                ],
                sorts: [
                ]
            }
        }
    }
</script>