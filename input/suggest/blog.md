Title: Suggest a Blog
---
To suggest a new blog, submit a PR to the [discoverdotnet](https://github.com/daveaglick/discoverdotnet) project manually or by clicking on this link: [https://github.com/daveaglick/discoverdotnet/new/master/input/data/blogs](https://github.com/daveaglick/discoverdotnet/new/master/input/data/blogs). You should add a new YAML file with a `.yml` extension that contains the following properties, all of which are optional unless indicated.

**Website**: The URL of the blog. If a feed was provided and this is omitted, the website will be taken from the feed.  
**Title**: The title of the blog. If a feed was provided and this is omitted, the title will be taken from the feed.  
**Description**: The description of the blog. If a feed was provided and this is omitted, the description will be taken from the feed.  
**Feed**: A URL to the feed for the blog.  
**FeedType**: `Atom` or `RSS` (`RSS` is assumed if this property is omitted).

For example, here's what the YAML file for [my blog](https://daveaglick.com) looks like:

```yaml
Feed: https://daveaglick.com/feed.rss
```