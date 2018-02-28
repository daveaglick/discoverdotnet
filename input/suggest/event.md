Title: Suggest An Event
---
To suggest a new event, submit a PR to the [discoverdotnet](https://github.com/daveaglick/discoverdotnet) project manually or by clicking on this link: [https://github.com/daveaglick/discoverdotnet/new/master/input/data/events](https://github.com/daveaglick/discoverdotnet/new/master/input/data/events). You should add a new YAML file with a `.yml` extension that contains the following properties, all of which are optional unless indicated.

**Website**: The URL of the event.
**Title**: The title of the event.
**Description**: A description of the event.
**Image**: A URL to an image or logo of the event.
**Location**: A plain text description of the location of the event.
**Country**: The localized country name where the event is located (used for filtering).
**Lat**: A rough latitude in decimal form of the event location used for mapping and proximity calculations. If you need to lookup the latitude you can use [GeoNames](http://www.geonames.org/).
**Lon**: A rough longitude in decimal form  of the event location used for mapping and proximity calculations. If you need to lookup the longitude you can use [GeoNames](http://www.geonames.org/).
**Date**: The date of the event in YYYY/MM/DD format.
**EndDate**: The end date of the event in YYYY/MM/DD format (if the event is more than one day long).
**Time**: The local time of the event in 24 hour HH:MM format.
**EndTime**: The local end time of the event in 24 hour HH:MM format (if there is a set end time).