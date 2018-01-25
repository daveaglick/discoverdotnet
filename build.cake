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
var issuesDir = Directory("./output.issues");

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

        /*
        Wyam(new WyamSettings
        {
            //UpdatePackages = true,
            ConfigurationFile = File("./config.wyam"),
            OutputPath = outputDir,
            Settings = new Dictionary<string, object>
            {
                { "GitHubToken", gitHubToken }
            }
        });
        */  

        StartProcess("../Wyam/src/clients/Wyam/bin/Debug/net462/wyam.exe",
            "-a \"../Wyam/src/**/bin/Debug/**/*.dll\""
            + $" --config \"config.wyam\""
            + $" --output \"{MakeAbsolute(outputDir).ToString()}\""
            + $" --setting GitHubToken=\"{gitHubToken}\"");
    });

Task("Issues")
    .Description("Generates the issue data for GitHub projects.")
    .IsDependentOn("Clean")
    .Does(() =>
    {
        var gitHubToken = EnvironmentVariable("DISCOVERDOTNET_GITHUB_TOKEN");

        /*
        Wyam(new WyamSettings
        {
            //UpdatePackages = true,
            ConfigurationFile = File("./issues.wyam"),
            OutputPath = issuesDir,
            Settings = new Dictionary<string, object>
            {
                { "GitHubToken", gitHubToken }
            }
        });  
        */        

        StartProcess("../Wyam/src/clients/Wyam/bin/Debug/net462/wyam.exe",
            "-a \"../Wyam/src/**/bin/Debug/**/*.dll\""
            + $" --config \"issues.wyam\""
            + $" --output \"{MakeAbsolute(issuesDir).ToString()}\""
            + $" --setting GitHubToken=\"{gitHubToken}\"");
    });

Task("Preview")
    .Description("Generates and previews the site.")
    .IsDependentOn("Issues")
    .Does(() =>
    {
        // Copy the issue data
        var issuesDataDir = outputDir + Directory("data/issues");
        EnsureDirectoryExists(issuesDataDir);
        CopyFiles("output.issues/*", issuesDataDir);

        // Generate the main site, but don't clean it out
        var gitHubToken = EnvironmentVariable("DISCOVERDOTNET_GITHUB_TOKEN");

        /*
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
                { "GitHubToken", gitHubToken }
            }
        });  
        */

        StartProcess("../Wyam/src/clients/Wyam/bin/Debug/net462/wyam.exe",
            "-a \"../Wyam/src/**/bin/Debug/**/*.dll\""
            + $" --config \"config.wyam\""
            + $" --output \"{MakeAbsolute(outputDir).ToString()}\""
            + " --noclean -p -w"
            + $" --setting GitHubToken=\"{gitHubToken}\"");
    });

Task("Publish")
    .Description("Generates and deploys the site.")
    .IsDependentOn("Build")
    .Does(() =>
    {
        Information("Deploying output to Netlify");
        var netlifyToken = EnvironmentVariable("DISCOVERDOTNET_NETLIFY_TOKEN");
        //var client = new NetlifyClient(netlifyToken);
        //client.UpdateSite("discoverdotnet.netlify.com", MakeAbsolute(outputDir).FullPath).SendAsync().Wait();
    });

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////
    
Task("Default")
    .IsDependentOn("Preview");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);