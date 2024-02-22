⚠ This repository is now archived. It was a fun ride, but as my own time has dwindled, the list of projects and blogs that Discover .NET indexes has fallen further and further behind. And that's not even considering all the pending functionality I had wanted to add as the site itself gets more stale. Now that builds have started to fail, and it would take some debugging to figure it out (time that I don't really have), it seems like as good a time as any to throw in the hat. At the moment it's just not a project I can commit to keep up to date. Thanks for all the submissions! I'll keep the code around as a reference for anyone who wants it or would like to do some data mining.

--

# Discover .NET

[https://discoverdot.net](https://discoverdot.net)

## Building

To build the site for previewing, you'll need to set the following environment variables:

* `DISCOVERDOTNET_GITHUB_TOKEN`, which can be obtained via creating a [personal access token](https://github.com/settings/tokens) with `public_repo` permissions.
* `DISCOVERDOTNET_MEETUP_TOKEN`, which can be obtained by visiting [the Meetup API key page](https://secure.meetup.com/meetup_api/key/).

Then run the following command:

```
build -target preview
```

This will build the site locally using a few items from each type of data and allow you to preview it (watch the output for the URL, but usually it's [http://localhost:5080](http://localhost:5080)).
