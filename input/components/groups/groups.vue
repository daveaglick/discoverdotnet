<template>
    <div>            
        <card-grid
            title="Groups"
            suggest-link="/suggest/group"
            card-json="/data/groups.json"
            :sorts="sorts"
            :randomizeOrder="true">
        </card-grid>
    </div>
</template>

<script>  
    module.exports = {
        data: function() {
            return {       
                lat: null,
                lon: null,         
                sorts: [
                    {
                        text: 'Closest',
                        sort: function(a, b) {
                            if(this.lat == null) {
                                if ("geolocation" in navigator) {
                                    var self = this;
                                    navigator.geolocation.getCurrentPosition(function(position) {
                                        self.lat = position.coords.latitude;
                                        self.lon = position.coords.longitude;
                                    });
                                } else {
                                    this.lat = "";
                                    /* geolocation IS NOT available */
                                }
                            }

                            // Do the sort
                            if(this.lat != "") {
                                if("lat" in a && "lon" in a) {
                                    if("lat" in b && "lon" in b) {
                                        return calculateDistance(this.lat, this.lon, a.lat, a.lon) -
                                            calculateDistance(this.lat, this.lon, b.lat, b.lon);
                                    } else {
                                        return 1;
                                    }
                                } else if("lat" in b && "lon" in b) {
                                    return -1;
                                } else {
                                    return 0;
                                }
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
    
    function calculateDistance(lat1, lon1, lat2, lon2) {
        var radlat1 = Math.PI * lat1/180;
        var radlat2 = Math.PI * lat2/180;
        var radlon1 = Math.PI * lon1/180;
        var radlon2 = Math.PI * lon2/180;
        var theta = lon1-lon2;
        var radtheta = Math.PI * theta/180;
        var dist = Math.sin(radlat1) * Math.sin(radlat2) + Math.cos(radlat1) * Math.cos(radlat2) * Math.cos(radtheta);
        dist = Math.acos(dist);
        dist = dist * 180/Math.PI;
        dist = dist * 60 * 1.1515;
        return dist;
    }
</script>
