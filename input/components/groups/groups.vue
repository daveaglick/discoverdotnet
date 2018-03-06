<template>
    <div>           
        <b-alert :show="geoAlert" dismissible variant="warning" @dismissed="geoAlert=false">
            Could not get your location.
        </b-alert>
        <card-grid
            title="Groups"
            suggest-link="/suggest/group"
            card-json="/data/groups.json"
            :filters="filters"
            :sorts="sorts"
            :randomizeOrder="true">
            <div slot="header" slot-scope="props">
                <v-map :zoom=3 :center="[41, -38]" style="height: 280px">
                    <v-tilelayer url="http://{s}.tile.osm.org/{z}/{x}/{y}.png"></v-tilelayer>
                    <v-marker-cluster>
                        <v-marker
                            v-for="cardData in props.filteredCardData"
                            :key="cardData"
                            v-if="cardData.lat && cardData.lon"
                            :lat-lng="[cardData.lat, cardData.lon]">
                            <v-popup :content="cardData.title"></v-popup>
                        </v-marker>
                    </v-marker-cluster>
                </v-map>
            </div>
        </card-grid>
    </div>
</template>

<script>  
    module.exports = {
        data: function() {
            return {       
                geoAlert: false,
                filters: [
                    {
                        text: 'Country',
                        field: 'country'
                    }
                ],
                sorts: [
                    {
                        text: 'Closest',
                        self: this,
                        apply: function(sort) {
                            var self = this.self;
                            if(currentLat == null) {
                                if ("geolocation" in navigator) {
                                    navigator.geolocation.getCurrentPosition(
                                        function(position) {
                                            currentLat = position.coords.latitude;
                                            currentLon = position.coords.longitude;
                                            sort();
                                        },
                                        function() {
                                            self.geoAlert = true;
                                        });
                                } else {
                                    self.geoAlert = true;
                                }
                            } else {                                
                                sort();
                            }
                        },
                        sort: function(a, b) {
                            if(currentLat != null) {
                                if(!("distance" in a)) {
                                    if("lat" in a && "lon" in a) {
                                        a.distance = calculateDistance(a.lat, a.lon);
                                    } else {
                                        a.distance = Number.MIN_VALUE;
                                    }
                                }
                                if(!("distance" in b)) {
                                    if("lat" in b && "lon" in b) {
                                        b.distance = calculateDistance(b.lat, b.lon);
                                    } else {
                                        b.distance = Number.MIN_VALUE;
                                    }
                                }
                                return a.distance - b.distance;
                            }
                        }
                    },
                    {
                        text: 'A-Z',
                        sort: function(a, b) {
                            return a.title.localeCompare(b.title);
                        }
                    },
                    {
                        text: 'Z-A',
                        sort: function(a, b) {      
                            return b.title.localeCompare(a.title);     
                        }
                    }
                ]
            }
        }
    }

    // Globals FTW!

    var currentLat = null;
    var currentLon = null;
    
    function calculateDistance(lat2, lon2) {
        var radlat1 = Math.PI * currentLat/180;
        var radlat2 = Math.PI * lat2/180;
        var radlon1 = Math.PI * currentLon/180;
        var radlon2 = Math.PI * lon2/180;
        var theta = currentLon-lon2;
        var radtheta = Math.PI * theta/180;
        var dist = Math.sin(radlat1) * Math.sin(radlat2) + Math.cos(radlat1) * Math.cos(radlat2) * Math.cos(radtheta);
        dist = Math.acos(dist);
        dist = dist * 180/Math.PI;
        dist = dist * 60 * 1.1515;
        return dist;
    }
</script>
