// The following environment variables need to be set:
// DISCOVERDOTNET_GITHUB_TOKEN
// DISCOVERDOTNET_NETLIFY_TOKEN
// DISCOVERDOTNET_MEETUP_TOKEN
// DISCOVERDOTNET_ALGOLIA_TOKEN

#tool nuget:?package=Wyam&version=2.2.7
#addin nuget:?package=Cake.Wyam&version=2.2.7
#addin nuget:?package=NetlifySharp&version=0.1.0
#addin nuget:?package=System.Runtime.Serialization.Formatters&version=4.3.0
#addin nuget:?package=Algolia.Search&version=5.0.0

using System.Reflection;
using NetlifySharp;
using Algolia.Search;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

var isLocal = BuildSystem.IsLocalBuild;
var isRunningOnAppVeyor = AppVeyor.IsRunningOnAppVeyor;
var isPullRequest = AppVeyor.Environment.PullRequest.IsPullRequest;
var buildNumber = AppVeyor.Environment.Build.Number;

var outputDir = Directory("./output");
var issuesDir = Directory("./output.issues");

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(context =>
{
    Information("Building Discover .NET");

    // See https://github.com/cake-build/cake/issues/2116
    AppDomain.CurrentDomain.AssemblyResolve += (_, eventArgs) =>
    {
        AssemblyName name = new AssemblyName(eventArgs.Name);
        Verbose($"Resolving assembly {eventArgs.Name}");
        Assembly assembly = AppDomain.CurrentDomain.GetAssemblies()
            .FirstOrDefault(x => !x.IsDynamic && x.GetName().Name == name.Name)
            ?? Assembly.Load(name.Name);       
        if(assembly != null)
        {
            Verbose($"Resolved by assembly {assembly.FullName}");
        }
        else
        {
            Verbose($"Assembly not resolved");
        }
        return assembly;
    };
});

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
    {
        CleanDirectories(new DirectoryPath[] { outputDir, issuesDir });
    });

Task("Build")
    .Description("Generates the site.")
    .IsDependentOn("Clean")
    .Does(() =>
    {
        var gitHubToken = EnvironmentVariable("DISCOVERDOTNET_GITHUB_TOKEN");
        var meetupToken = EnvironmentVariable("DISCOVERDOTNET_MEETUP_TOKEN");

        Wyam(new WyamSettings
        {
            //UpdatePackages = true,
            ConfigurationFile = File("./config.wyam"),
            OutputPath = outputDir,
            Settings = new Dictionary<string, object>
            {
                { "GitHubToken", gitHubToken },
                { "MeetupToken", meetupToken }
            }
        });

        //StartProcess("../Wyam/src/clients/Wyam/bin/Debug/net462/wyam.exe",
        //    "-a \"../Wyam/tests/integration/Wyam.Examples.Tests/bin/Debug/net462/**/*.dll\""
        //    + $" --config \"config.wyam\""
        //    + $" --output \"{MakeAbsolute(outputDir).ToString()}\""
        //    + $" --setting GitHubToken=\"{gitHubToken}\""
        //    + $" --setting MeetupToken=\"{meetupToken}\"");
    });

Task("Issues")
    .Description("Generates the issue data for GitHub projects.")
    .IsDependentOn("Clean")
    .Does(() =>
    {
        var gitHubToken = EnvironmentVariable("DISCOVERDOTNET_GITHUB_TOKEN");

        Wyam(new WyamSettings
        {
            //UpdatePackages = true,
            ConfigurationFile = File("./issues.wyam"),
            OutputPath = issuesDir,
            Settings = new Dictionary<string, object>
            {
                { "GitHubToken", gitHubToken },
                { "Preview", string.Equals(target, "Preview", StringComparison.OrdinalIgnoreCase ) }
            }
        });       
      
        // StartProcess("../Wyam/src/clients/Wyam/bin/Debug/net462/wyam.exe",
        //    "-a \"../Wyam/tests/integration/Wyam.Examples.Tests/bin/Debug/net462/**/*.dll\""
        //    + $" --config \"issues.wyam\""
        //    + $" --output \"{MakeAbsolute(issuesDir).ToString()}\""
        //    + $" --setting GitHubToken=\"{gitHubToken}\""
        //    + $" --setting Preview=\"{ string.Equals(target, "Preview", StringComparison.OrdinalIgnoreCase) }\"");
    });

Task("Preview")
    .Description("Generates and previews the site.")
    .IsDependentOn("Issues")
    .Does(() =>
    {
        // Copy the issue data
        var issuesDataDir = outputDir + Directory("data/issues");
        EnsureDirectoryExists(issuesDataDir);
        CopyFiles("output.issues/**/*", issuesDataDir, true);

        // Generate the main site, but don't clean it out
        var gitHubToken = EnvironmentVariable("DISCOVERDOTNET_GITHUB_TOKEN");
        var meetupToken = EnvironmentVariable("DISCOVERDOTNET_MEETUP_TOKEN");

        Wyam(new WyamSettings
        {
            //UpdatePackages = true,
            ConfigurationFile = File("./config.wyam"),
            OutputPath = outputDir,
            NoClean = true,
            Preview = true,
            Watch = true,
            Settings = new Dictionary<string, object>
            {
                { "GitHubToken", gitHubToken },
                { "MeetupToken", meetupToken },
                { "Preview", true }
            },
            ContentTypes = new Dictionary<string, string>
            {
                { ".vue", "text/plain" }
            }
        });  

        // StartProcess("../Wyam/src/clients/Wyam/bin/Debug/net462/wyam.exe",
        //    "-a \"../Wyam/tests/integration/Wyam.Examples.Tests/bin/Debug/net462/**/*.dll\""
        //    + $" --config \"config.wyam\""
        //    + $" --output \"{MakeAbsolute(outputDir).ToString()}\""
        //    + " --noclean -p -w"
        //    + $" --setting GitHubToken=\"{gitHubToken}\""
        //    + $" --setting MeetupToken=\"{meetupToken}\""
        //    + $" --setting Preview=\"true\""
        //    + $" --content-type .vue=text/plain");
    });

Task("Publish-Issues")
    .Description("Generates and deploys the issue data.")
    .IsDependentOn("Issues")
    .Does(() =>
    {
        Information("Deploying issue data to Netlify");
        var netlifyToken = EnvironmentVariable("DISCOVERDOTNET_NETLIFY_TOKEN");
        var client = new NetlifyClient(netlifyToken);
        client.UpdateSite("discoverdotnet-issues.netlify.com", MakeAbsolute(issuesDir).FullPath).SendAsync().Wait();
        
        Information("Updating search indexes");
        var algoliaToken = EnvironmentVariable("DISCOVERDOTNET_ALGOLIA_TOKEN");
        var algoliaClient = new AlgoliaClient("7TKEQH0O12", algoliaToken);
        UpdateSearchIndex(algoliaClient, "issues", System.IO.Path.Combine(MakeAbsolute(issuesDir).FullPath, "search.json"));
    });

Task("Publish-Site")
    .Description("Generates and deploys the site.")
    .IsDependentOn("Build")
    .Does(() =>
    {
        Information("Deploying site to Netlify");
        var netlifyToken = EnvironmentVariable("DISCOVERDOTNET_NETLIFY_TOKEN");
        var client = new NetlifyClient(netlifyToken);
        client.UpdateSite("discoverdotnet.netlify.com", MakeAbsolute(outputDir).FullPath).SendAsync().Wait();
        
        Information("Updating search indexes");
        var algoliaToken = EnvironmentVariable("DISCOVERDOTNET_ALGOLIA_TOKEN");
        var algoliaClient = new AlgoliaClient("7TKEQH0O12", algoliaToken);
        string indexPath = System.IO.Path.Combine(MakeAbsolute(outputDir).FullPath, "data", "search");
        UpdateSearchIndex(algoliaClient, "projects", System.IO.Path.Combine(indexPath, "projects.json"));
        UpdateSearchIndex(algoliaClient, "blogs", System.IO.Path.Combine(indexPath, "blogs.json"));
        UpdateSearchIndex(algoliaClient, "posts", System.IO.Path.Combine(indexPath, "posts.json"));
        UpdateSearchIndex(algoliaClient, "broadcasts", System.IO.Path.Combine(indexPath, "broadcasts.json"));
        UpdateSearchIndex(algoliaClient, "episodes", System.IO.Path.Combine(indexPath, "episodes.json"));
        UpdateSearchIndex(algoliaClient, "resources", System.IO.Path.Combine(indexPath, "resources.json"));
    });

//////////////////////////////////////////////////////////////////////
// HELPERS
//////////////////////////////////////////////////////////////////////

public class SearchIndexItemEqualityComparer : IEqualityComparer<JObject>
{
	public bool Equals(JObject a, JObject b)
	{
		return a.Properties().Where(x => x.Name != "objectID").Select(x => (x.Name, x.Value.ToString())).OrderBy(x => x.Name)
			.SequenceEqual(b.Properties().Where(x => x.Name != "objectID").Select(x => (x.Name, x.Value.ToString())).OrderBy(x => x.Name));
	}

	public int GetHashCode(JObject obj)
	{
		int hash = 27;
		foreach (var prop in obj.Properties().Where(x => x.Name != "objectID").Select(x => (x.Name, x.Value.ToString())).OrderBy(x => x.Name))
		{
			hash = (13 * hash) + prop.Name.GetHashCode();
			hash = (13 * hash) + prop.Item2.GetHashCode();
		}
		return hash;
	}
}

public void UpdateSearchIndex(AlgoliaClient client, string indexName, string path)
{
	// Read from Algolia
	Index index = client.InitIndex(indexName);
	Query query = new Query("");
	Index.IndexIterator results = index.BrowseAll(query);
	HashSet<JObject> existing = new HashSet<JObject>(new SearchIndexItemEqualityComparer());
	foreach (JObject result in results)
	{
		existing.Add(result);
	}

	// Read from the file
	List<JObject> adds = new List<JObject>();
	JArray file = JArray.Parse(System.IO.File.ReadAllText(path));
	foreach(JObject item in file)
	{
		// Is it already in the index?
		if(!existing.Remove(item))
		{
			// It wasn't matched, so this is a new one
			adds.Add(item);
		}
	}
	
    Information($"Search index \"{indexName}\": deleting {existing.Count}, adding {adds.Count}");
	
	// Delete
	index.DeleteObjects(existing.Select(x => x.Property("objectID").Value.ToString()));
	
	// Add
	index.AddObjects(adds);
}

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////
    
Task("Default")
    .IsDependentOn("Preview");
    
Task("Publish")
    .IsDependentOn("Publish-Issues")
    .IsDependentOn("Publish-Site");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);