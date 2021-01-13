using System;
using Statiq.Common;

namespace DiscoverDotnet.Models
{
    public class PagedIssue
    {
        private readonly Issue _issue;

        public string ProjectKey { get; }
        public DateTimeOffset CreatedAt => _issue.CreatedAt;
        public DateTimeOffset UpdatedAt => _issue.UpdatedAt;
        public string Link => _issue.Link;
        public string Title => _issue.Title;
        public string[] Labels => _issue.Labels;

        public PagedIssue(Issue issue, IDocument document)
        {
            _issue = issue;
            ProjectKey = document.GetString("ProjectKey");
        }
    }
}
