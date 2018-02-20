Title: Suggest A Group
---
To suggest a new group, submit a PR to the [discoverdotnet](https://github.com/daveaglick/discoverdotnet) project manually or by clicking on this link: [https://github.com/daveaglick/discoverdotnet/new/master/input/data/groups](https://github.com/daveaglick/discoverdotnet/new/master/input/data/groups). You should add a new YAML file with a `.yml` extension that contains the following properties, all of which are optional unless indicated.

Note that the Meetup API is used to find and automatically add all groups with a topic of ".NET". If your group is on Meetup and uses the ".NET" topic you don't need to supply a YAML file for it.

**Website**: The URL of the group. If the group is on Meetup, please use that URL so we can get more data from the Meetup API.
**Title**: The title of the group. If the website is a link to Meetup and this is omitted, then the title will be taken from the Meetup API.
**Image**: A URL to an image or logo of the group. If the website is a link to Meetup and this is omitted, then the image will be taken from the Meetup API.
**Location**: A plain text description of the location of the group. If the website is a link to Meetup and this is omitted, then the location will be taken from the Meetup API.
**Country**: The localized country name where the group is located (used for filtering).
**Lat**: A rough latitude of the group location used for mapping and proximity calculations. If the website is a link to Meetup and this is omitted, then the latitude will be taken from the Meetup API. If you need to lookup the latitude you can use [GeoNames](http://www.geonames.org/).
**Lon**: A rough longitude of the group location used for mapping and proximity calculations. If the website is a link to Meetup and this is omitted, then the longitude will be taken from the Meetup API. If you need to lookup the longitude you can use [GeoNames](http://www.geonames.org/).