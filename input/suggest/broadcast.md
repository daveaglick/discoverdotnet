Title: Suggest A Broadcast
---
To suggest a new broadcast (podcast, video series, etc.), submit a PR to the [discoverdotnet](https://github.com/daveaglick/discoverdotnet) project manually or by clicking on this link: [https://github.com/daveaglick/discoverdotnet/new/master/input/data/broadcasts](https://github.com/daveaglick/discoverdotnet/new/master/input/data/broadcasts). You should add a new YAML file with a `.yml` extension that contains the following properties, all of which are optional unless indicated.

**Website**: The URL of the broadcast. If a feed was provided and this is omitted, the website will be taken from the feed.  
**Title**: The title of the broadcast. If a feed was provided and this is omitted, the title will be taken from the feed.  
**Image**: A URL to an image or logo of the broadcast.
**Description**: The description of the broadcast. If a feed was provided and this is omitted, the description will be taken from the feed.  
**Feed**: A URL to the feed for the broadcast.  
**FeedType**: `Atom` or `RSS` (`RSS` is assumed if this property is omitted).

For example, here's what the YAML file for the [.NET Rocks!](http://www.dotnetrocks.com) podcast looks like:

```yaml
Feed: http://www.pwop.com/feed.aspx?show=dotnetrocks&filetype=master
```