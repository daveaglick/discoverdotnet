using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Octokit;
using Statiq.Common;

namespace DiscoverDotnet.Modules
{
    public class GetIssueGitHubData : Module
    {
        private static readonly DateTimeOffset OneDayAgo = DateTimeOffset.Now.AddHours(-24);

        private GitHubManager _gitHub;
        private FoundationManager _foundation;

        protected override async Task BeforeExecutionAsync(IExecutionContext context)
        {
            if (_gitHub == null)
            {
                _gitHub = context.GetRequiredService<GitHubManager>();
            }
            if (_foundation == null)
            {
                _foundation = context.GetRequiredService<FoundationManager>();
                await _foundation.PopulateAsync(context);
            }
        }

        protected override async Task<IEnumerable<IDocument>> ExecuteInputAsync(IDocument input, IExecutionContext context)
        {
            IDocument output = input;

            // Extract the GitHub owner and name
            if (Uri.TryCreate(input.GetString("SourceCode"), UriKind.Absolute, out Uri sourceCode)
                && sourceCode.Host.EndsWith("github.com", StringComparison.OrdinalIgnoreCase))
            {
                string owner = sourceCode.Segments[1].Trim('/');
                string name = sourceCode.Segments[2].Trim('/');

                // Get issue data
                context.LogInformation($"Getting GitHub issue data for {owner}/{name}");
                IReadOnlyList<Issue> issues = await _gitHub.GetAsync(
                    x => x.Issue.GetAllForRepository(
                        owner,
                        name,
                        new RepositoryIssueRequest
                        {
                            Filter = IssueFilter.All
                        },
                        new ApiOptions
                        {
                            PageSize = 100
                        }),
                    context);
                issues = issues
                    .Where(x => x.PullRequest == null)
                    .ToList();

                // Get the metadata and document
                if (issues.Count > 0)
                {
                    Models.Issue[] issueData = issues
                        .Select(x => new Models.Issue(x, OneDayAgo))
                        .OrderByDescending(x => x.CreatedAt)
                        .ToArray();
                    MetadataItems metadata = new MetadataItems
                    {
                        { "GitHubOwner", owner },
                        { "GitHubName", name },
                        { "Issues", issueData },
                        { "IssuesCount", issueData.Length },
                        { "RecentIssuesCount", issueData.Count(x => x.Recent) },
                        { "HelpWantedIssuesCount", issueData.Count(x => x.HelpWanted) }
                    };
                    if (!input.ContainsKey("Microsoft") && GitHubManager.MicrosoftOwners.Contains(owner, StringComparer.OrdinalIgnoreCase))
                    {
                        metadata.Add("Microsoft", true);
                    }
                    if (!input.ContainsKey("Foundation") && _foundation.IsInFoundation(owner, name))
                    {
                        metadata.Add("Foundation", true);
                    }

                    output = input.Clone(metadata);
                }
            }

            return output.Yield();
        }
    }
}
