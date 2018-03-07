<template>
    <v-map :zoom=1 :center="[41, -38]" style="height: 280px">
        <v-tilelayer url="http://{s}.tile.osm.org/{z}/{x}/{y}.png"></v-tilelayer>
        <v-marker-cluster>
            <v-marker
                v-for="cardData in filteredCardData"
                :key="cardData"
                v-if="cardData.lat && cardData.lon"
                :lat-lng="[cardData.lat, cardData.lon]">
                <v-popup :content="popup(cardData)"></v-popup>
            </v-marker>
        </v-marker-cluster>
    </v-map>
</template>

<script>
    module.exports = {
        props: [
            'filteredCardData'
        ],
        methods: {
            popup: function(cardData) {
                var popup = "<div class='font-weight-bold'>";
                if('website' in cardData) {
                    popup = popup + "<a href='" + cardData.website + "'>";
                }
                popup = popup + cardData.title;
                if('website' in cardData) {
                    popup = popup + "</a>";
                }
                popup = popup + "</div>";
                if('location' in cardData) {
                    popup = popup + "<div>" + cardData.location + "</div>";
                }
                return popup;
            }
        }
    }
</script>