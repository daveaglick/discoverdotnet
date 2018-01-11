// The following environment variables need to be set:
// DISCOVERDOTNET_GITHUB_TOKEN
// DISCOVERDOTNET_NETLIFY_TOKEN

#tool "Wyam"
#addin "Cake.Wyam"
#addin "NetlifySharp"
#addin "Newtonsoft.Json"
#addin "System.Runtime.Serialization.Formatters"

using NetlifySharp;

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

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(context =>
{
    Information("Building Discover .NET");
});

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Build")
    .Description("Generates the site.")
    .Does(() =>
    {
        Wyam(new WyamSettings
        {
            UpdatePackages = true
        });  
    });

Task("Preview")
    .Description("Generates and previews the site.")
    .Does(() =>
    {
        Wyam(new WyamSettings
        {
            Preview = true,
            Watch = true,
            UpdatePackages = true
        });  
    });

Task("Publish")
    .Description("Generates and deploys the site.")
    .IsDependentOn("Build")
    .Does(() =>
    {
        Information("Deploying output to Netlify");
        var netlifyToken = EnvironmentVariable("DISCOVERDOTNET_NETLIFY_TOKEN");
        var client = new NetlifyClient(netlifyToken);
        client.UpdateSite("discoverdotnet.netlify.com", MakeAbsolute(outputDir).FullPath).SendAsync().Wait();
    });


//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////
    
Task("Default")
    .IsDependentOn("Build");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);