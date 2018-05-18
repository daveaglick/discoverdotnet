Title: API
---
Use the data that powers Discover .NET to create awesome things! If some data that you want to use isn't available here or isn't in the right format for you, please [open an issue](https://github.com/daveaglick/discoverdotnet/issues) and let us know what you want to do and a special endpoint can probably be created.

# Attribution

While not required, a link back to Discover .NET would be nice:

<div class="alert alert-secondary text-center">
Data by <a href="https://discoverdot.net" class="alert-link">Discover .NET</a>
</div>

# Endpoints

All data is provided as JSON files. No authentication or rate limits apply (that's the beauty of static sites).

* [Projects](#projects)
* [Aggregate Issues](#aggregate-issues)
* [Blogs](#blogs)
* [Broadcasts](#broadcasts)
* [Groups](#groups)
* [Events](#events)
* [Resources](#resources)
* [Miscellaneous](#miscellaneous)

---

### Projects

Get data for all projects:

`GET https://discoverdot.net/data/projects.json`

Example response:

```json
[
    {
        "key": "project-key",
        "title": "Project Title",
        "link": "/projects/project-key",
        "image": "https://domain.com/someimagelink.png",
        "nuGet": "PackageName",
        "source": "https://github.com/user/project",
        "description": "The project description.",
        "stargazersCount": 123,
        "forksCount": 123,
        "openIssuesCount": 123,
        "pushedAt": "2018-05-15T15:46:10+00:00",
        "website": "https://www.projectsite.com",
        "donations": "https://www.donationssite.com",
        "language": "C#",
        "tags": [
            "One Tag",
            "Another Tag"
        ],
        "discoveryDate": "2018-05-15T15:46:10+00:00",
        "comment": "A comment if it exists.",
        "platform": true,
        "microsoft": true,
        "foundation": true
    },
    {
        ...
    }
]
```

---

Get data for a specific project:

`GET https://discoverdot.net/data/projects/[project-key].json`

Example response:

```json
{
    "key": "project-key",
    "title": "Project Title",
    "link": "/projects/project-key",
    "image": "https://domain.com/someimagelink.png",
    "nuGet": "PackageName",
    "source": "https://github.com/user/project",
    "description": "The project description.",
    "stargazersCount": 123,
    "forksCount": 123,
    "openIssuesCount": 123,
    "pushedAt": "2018-05-15T15:46:10+00:00",
    "website": "https://www.projectsite.com",
    "donations": "https://www.donationssite.com",
    "language": "C#",
    "tags": [
        "One Tag",
        "Another Tag"
    ],
    "discoveryDate": "2018-05-15T15:46:10+00:00",
    "comment": "A comment if it exists.",
    "platform": true,
    "microsoft": true,
    "foundation": true
}
```

---

Get the total number of open issues for all projects:

`GET https://discoverdot.net/data/issues/project-counts.json`

Example response:

```json
[
    {
        "projectKey": "project-key",
        "issuesCount": 159,
        "recentIssuesCount": 5,
        "helpWantedIssuesCount": 78
    },
    {
        ...
    }
]
```

---

Get the open issues for a specific project:

`GET https://discoverdot.net/data/issues/projects/[project-key].json`

Example response:

```json
[
    {
        "createdAt": "2018-05-18T18:46:23+00:00",
        "updatedAt": "2018-05-18T18:46:23+00:00",
        "link": "https://github.com/user/project/issues/1377",
        "title": "Issue title",
        "labels":
        [
            "Label One",
            "Another Label"
        ],
        "recent": true,
        "helpWanted": false
    },
    {
        ...
    }
]
```

---

### Aggregate Issues

Because of their large numbers, aggregate issues are paged with 24 issues per page.

Get the total number of open issues for each set of pages:

`GET https://discoverdot.net/data/issues/total-counts.json`

Example response:

```json
{
    "all": 9700,
    "helpWanted": 1080,
    "microsoft": 2754,
    "foundation": 3764,
    "platform": 1578
}
```

---

Get all open issues in pages of 24 issues per page (0-based page numbers):

`GET https://discoverdot.net/data/issues/all/[page-number].json`

Example response:

```json
[
    {
        "createdAt": "2018-05-18T18:46:23+00:00",
        "updatedAt": "2018-05-18T18:46:23+00:00",
        "link": "https://github.com/user/project/issues/1377",
        "title": "Issue title",
        "labels":
        [
            "Label One",
            "Another Label"
        ],
        "recent": true,
        "helpWanted": false
    },
    {
        ...
    }
]
```

---

Get open help wanted issues in pages of 24 issues per page (0-based page numbers):

`GET https://discoverdot.net/data/issues/help-wanted/[page-number].json`

Example response:

```json
[
    {
        "createdAt": "2018-05-18T18:46:23+00:00",
        "updatedAt": "2018-05-18T18:46:23+00:00",
        "link": "https://github.com/user/project/issues/1377",
        "title": "Issue title",
        "labels":
        [
            "Label One",
            "Another Label"
        ],
        "recent": true,
        "helpWanted": false
    },
    {
        ...
    }
]
```

---

Get open recent issues in pages of 24 issues per page (0-based page numbers):

`GET https://discoverdot.net/data/issues/recent/[page-number].json`

Example response:

```json
[
    {
        "createdAt": "2018-05-18T18:46:23+00:00",
        "updatedAt": "2018-05-18T18:46:23+00:00",
        "link": "https://github.com/user/project/issues/1377",
        "title": "Issue title",
        "labels":
        [
            "Label One",
            "Another Label"
        ],
        "recent": true,
        "helpWanted": false
    },
    {
        ...
    }
]
```

---

Get open issues from Microsoft projects in pages of 24 issues per page (0-based page numbers):

`GET https://discoverdot.net/data/issues/microsoft/[page-number].json`

Example response:

```json
[
    {
        "createdAt": "2018-05-18T18:46:23+00:00",
        "updatedAt": "2018-05-18T18:46:23+00:00",
        "link": "https://github.com/user/project/issues/1377",
        "title": "Issue title",
        "labels":
        [
            "Label One",
            "Another Label"
        ],
        "recent": true,
        "helpWanted": false
    },
    {
        ...
    }
]
```

---

Get open issues from .NET Foundation projects in pages of 24 issues per page (0-based page numbers):

`GET https://discoverdot.net/data/issues/foundation/[page-number].json`

Example response:

```json
[
    {
        "createdAt": "2018-05-18T18:46:23+00:00",
        "updatedAt": "2018-05-18T18:46:23+00:00",
        "link": "https://github.com/user/project/issues/1377",
        "title": "Issue title",
        "labels":
        [
            "Label One",
            "Another Label"
        ],
        "recent": true,
        "helpWanted": false
    },
    {
        ...
    }
]
```

---

Get open issues from .NET Platform projects in pages of 24 issues per page (0-based page numbers):

`GET https://discoverdot.net/data/issues/platform/[page-number].json`

Example response:

```json
[
    {
        "createdAt": "2018-05-18T18:46:23+00:00",
        "updatedAt": "2018-05-18T18:46:23+00:00",
        "link": "https://github.com/user/project/issues/1377",
        "title": "Issue title",
        "labels":
        [
            "Label One",
            "Another Label"
        ],
        "recent": true,
        "helpWanted": false
    },
    {
        ...
    }
]
```

---

### Blogs

Get data for all blogs:

`GET https://discoverdot.net/data/blogs.json`

Example response:

```json
[
    {
        "key": "blog-key",
        "title": "Blog Title",
        "image": "https://domain.com/someimagelink.png",
        "link": "/blogs/blog-key",
        "description": "The blog description.",
        "author": "Au Thor",
        "website": "https://www.blogsite.com",
        "feed": "https://www.blogsite.com/feed",
        "lastPublished": "2018-05-15T15:46:10+00:00",
        "newestFeedItem": {
            "title": "The Title of the Blog Post",
            "link": "https://www.blogsite.com/link-to-post/",
            "description": "A description of the post.",
            "published": "2017-08-16T00:00:00+00:00",
            "recent": false,
            "links": {
                "text/html": "https://www.blogsite.com/link-to-post/"
            },
            "author": "Au Thor"
        }
    },
    {
        ...
    }
]
```

---

Get data for a specific blog:

`GET https://discoverdot.net/data/blogs/[blog-key].json`

Example response:

```json
{
    "key": "blog-key",
    "title": "Blog Title",
    "image": "https://domain.com/someimagelink.png",
    "link": "/blogs/blog-key",
    "description": "The blog description.",
    "author": "Au Thor",
    "website": "https://www.blogsite.com",
    "feed": "https://www.blogsite.com/feed",
    "lastPublished": "2018-05-15T15:46:10+00:00",
    "newestFeedItem": {
        "title": "The Title of the Blog Post",
        "link": "https://www.blogsite.com/link-to-post/",
        "description": "A description of the post.",
        "published": "2017-08-16T00:00:00+00:00",
        "recent": false,
        "links": {
            "text/html": "https://www.blogsite.com/link-to-post/"
        },
        "author": "Au Thor"
    }
}
```

---

Get posts for a specific blog:

`GET https://discoverdot.net/data/posts/[blog-key].json`

Example response:

```json
[
    {
        "title": "The Title of the Blog Post",
        "link": "https://www.blogsite.com/link-to-post/",
        "description": "A description of the post.",
        "published": "2017-08-16T00:00:00+00:00",
        "recent": false,
        "links": {
            "text/html": "https://www.blogsite.com/link-to-post/"
        },
        "author": "Au Thor"
    },
    {
        ...
    }
]
```

---

### Broadcasts

Get data for all broadcasts:

`GET https://discoverdot.net/data/broadcasts.json`

Example response:

```json
[
    {
        "key": "broadcast-key",
        "title": "Broadcast Title",
        "image": "https://domain.com/someimagelink.png",
        "link": "/broadcasts/broadcast-key",
        "description": "The broadcast description.",
        "author": "Au Thor",
        "website": "https://www.broadcastsite.com",
        "feed": "https://www.broadcastsite.com/feed",
        "lastPublished": "2018-05-15T15:46:10+00:00",
        "newestFeedItem": {
            "title": "The Title of the Broadcast Post",
            "link": "https://www.broadcastsite.com/link-to-post/",
            "description": "A description of the post.",
            "published": "2017-08-16T00:00:00+00:00",
            "recent": false,
            "links": {
                "text/html": "https://www.broadcastsite.com/link-to-post/"
            },
            "author": "Au Thor"
        }
    },
    {
        ...
    }
]
```

---

Get data for a specific broadcast:

`GET https://discoverdot.net/data/broadcasts/[broadcast-key].json`

Example response:

```json
{
    "key": "broadcast-key",
    "title": "Broadcast Title",
    "image": "https://domain.com/someimagelink.png",
    "link": "/broadcasts/broadcast-key",
    "description": "The broadcast description.",
    "author": "Au Thor",
    "website": "https://www.broadcastsite.com",
    "feed": "https://www.broadcastsite.com/feed",
    "lastPublished": "2018-05-15T15:46:10+00:00",
    "newestFeedItem": {
        "title": "The Title of the Broadcast Post",
        "link": "https://www.broadcastsite.com/link-to-post/",
        "description": "A description of the post.",
        "published": "2017-08-16T00:00:00+00:00",
        "recent": false,
        "links": {
            "text/html": "https://www.broadcastsite.com/link-to-post/"
        },
        "author": "Au Thor"
    }
}
```

---

Get episodes for a specific broadcast:

`GET https://discoverdot.net/data/episodes/[broadcast-key].json`

Example response:

```json
[
    {
        "title": "The Title of the Broadcast Post",
        "link": "https://www.broadcastsite.com/link-to-post/",
        "description": "A description of the post.",
        "published": "2017-08-16T00:00:00+00:00",
        "recent": false,
        "links": {
            "text/html": "https://www.broadcastsite.com/link-to-post/"
        },
        "author": "Au Thor"
    },
    {
        ...
    }
]
```

---

### Groups

Get data for all groups:

`GET https://discoverdot.net/data/groups.json`

Example response:

```json
[
    {
        "website": "https://www.meetup.com/dotnetcambridge/",
        "title": ".NET Cambridge",
        "image": "https://secure.meetupstatic.com/photos/event/1/1/0/3/600_461884355.jpeg",
        "location": "Cambridge, United Kingdom",
        "country": "United Kingdom",
        "lat": "52.23",
        "lon": "0.15",
        "cardType": "Group",
        "nextEventWebsite": "https://www.meetup.com/dotnetcambridge/events/hklrnpyxhblc",
        "nextEventTitle": "From Modular-Monolith to Micro-service and The State of the Function",
        "nextEventDate": "2018/05/21",
        "nextEventTime": "18:45"
    },
    {
        ...
    }
]
```

---

### Events

Get data for all events:

`GET https://discoverdot.net/data/events.json`

Example response:

```json
[
    {
        "website": "http://www.devsum.se/",
        "title": "DevSum18",
        "description": "At DevSum the focus is the latest trends and technologies.",
        "image": "http://www.devsum.se/wp-content/uploads/2017/09/devsum18_196x60_header-vit-1.png",
        "location": "Stockholm, Sweden",
        "country": "Sweden",
        "lat": "59.19",
        "lon": "18.03",
        "date": "2018/05/31",
        "endDate": "2018/06/01",
        "time": "18:45",
        "endTime": "20:00",
        "cardType":"Event"
    },
    {
        ...
    }
]
```

---

### Resources

Get data for all resources:

`GET https://discoverdot.net/data/resources.json`

Example response:

```json
[
    {
        "website": "https://www.telerik.com/products/decompiler.aspx",
        "title": "JustDecompile",
        "description": "JustDecompile is a free Telerik tool for .NET assembly browsing and decompiling.",
        "cardType": "Resource",
        "tags":
        [
            "Tool",
            "Decompilation"
        ]
    },
    {
        ...
    }
]
```

---

### Miscellaneous

Get recent news (blog posts and broadcast episodes):

`GET https://discoverdot.net/data/news.json`

Example response:

```json
[
    {
        "feedLink": "/blogs/aspnet",
        "feedTitle": "ASP.NET Blog",
        "title": "The Title of the Blog Post",
        "link": "https://www.blogsite.com/link-to-post/",
        "published": "2017-08-16T00:00:00+00:00",
        "links": {
            "text/html": "https://www.blogsite.com/link-to-post/"
        },
        "author": "Au Thor"
    },
    {
        ...
    }
]
```