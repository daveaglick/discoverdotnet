// The following environment variables need to be set:
// DISCOVERDOTNET_GITHUB_TOKEN
// DISCOVERDOTNET_NETLIFY_TOKEN
// DISCOVERDOTNET_MEETUP_TOKEN

#tool nuget:?package=Wyam&version=1.4.1
#addin nuget:?package=Cake.Wyam&version=1.4.1
#addin nuget:?package=NetlifySharp&version=0.1.0
#addin nuget:?package=System.Runtime.Serialization.Formatters&version=4.3.0

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
    });

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