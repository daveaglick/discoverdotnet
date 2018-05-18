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
            :randomize-order="true">
                <card-map slot="header" slot-scope="props" :filtered-card-data="props.filteredCardData"></card-map>
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
                            return a.title ? a.title.localeCompare(b.title) : -1;
                        }
                    },
                    {
                        text: 'Z-A',
                        sort: function(a, b) {      
                            return b.title ? b.title.localeCompare(a.title) : -1;
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
