using System;
using System.Linq;
using AngleSharp.Dom;

namespace DiscoverDotnet.Models
{
    public class Issue
    {
        private static readonly string[] HelpWantedLabels = new[]
        {
            "helpwanted",
            "goodfirstissue",
            "firsttimersonly",
            "upforgrabs",
            "newcontributor",
            "goodfirsttask"
        };

        public DateTimeOffset CreatedAt { get; }
        public DateTimeOffset UpdatedAt { get; }
        public string Link { get; }
        public string Title { get; }
        public string[] Labels { get; }
        public bool Recent { get; }
        public bool HelpWanted { get; }

        public Issue(Octokit.Issue issue, DateTimeOffset oneDayAgo)
        {
            CreatedAt = issue.CreatedAt;
            UpdatedAt = issue.UpdatedAt ?? issue.CreatedAt;
            Link = issue.HtmlUrl;
            Title = issue.Title;
            Labels = issue.Labels.Select(x => x.Name).ToArray();
            Recent = CreatedAt > oneDayAgo;
            HelpWanted = Labels
                .Select(l => new string(l.Where(c => char.IsLetterOrDigit(c)).ToArray()))
                .Any(l => HelpWantedLabels.Contains(l, StringComparer.OrdinalIgnoreCase));
        }
    }
}
