# Discover .NET

[https://discoverdot.net](https://discoverdot.net)

## Building

To build the site for previewing, you'll need to set the following environment variables:

```
DISCOVERDOTNET_GITHUB_TOKEN
DISCOVERDOTNET_MEETUP_TOKEN
```

Then run the following command:

```
build -target preview
```

This will build the site locally using a few items from each type of data and allow you to preview it (watch the output for the URL, but usually it's [http://localhost:5080](http://localhost:5080)).
