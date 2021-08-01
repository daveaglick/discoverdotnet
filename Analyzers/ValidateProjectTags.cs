using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscoverDotnet.Pipelines.Projects;
using Microsoft.Extensions.Logging;
using Statiq.Common;

namespace DiscoverDotnet.Analyzers
{
    public class ValidateProjectTags : DataAnalyzer
    {
        public static HashSet<string> Tags = new HashSet<string>
        {
            "3D",
            "API",
            "API Client",
            "ASP.NET",
            "AWS",
            "Algorithms",
            "Android",
            "App",
            "Application Framework",
            "Assembly Manipulation",
            "Assets",
            "Azure",
            "BDD",
            "Blazor",
            "Blockchain",
            "Bot",
            "Build Tool",
            "CLI",
            "CMS",
            "CSS",
            "Caching",
            "Code Analysis",
            "Code Generation",
            "Commerce",
            "Compiler",
            "Compression",
            "Configuration",
            "Containers",
            "Cryptography",
            "DDD",
            "Data Access",
            "Data Formats",
            "Data Mapping",
            "Data Science",
            "Database",
            "Database Tools",
            "Date/Time",
            "Debugging",
            "Decompilation",
            "Deployment",
            "DevOps",
            "Distributed Computing",
            "Documentation",
            "ETL",
            "Error Handling",
            "Event Sourcing",
            "Examples",
            "Extensibility",
            "Formatting",
            "GIS",
            "GUI",
            "Game Development",
            "Games",
            "GCP",
            "Git",
            "Global Tool",
            "Graphics",
            "HTML",
            "HTTP",
            "Hardware",
            "IDE",
            "IO",
            "Installation",
            "Interoperability",
            "IoC",
            "IoT",
            "JavaScript",
            "LINQ",
            "Localization",
            "Logging",
            "MSBuild",
            "MVVM",
            "Mail",
            "Markdown",
            "Media",
            "Messaging",
            "Metaprogramming",
            "Minification",
            "Monitoring",
            "Networking",
            "NuGet",
            "ORM",
            "Office",
            "PDF",
            "Package Manager",
            "Parsing",
            "Performance",
            "Productivity",
            "Profiling",
            "Razor",
            "Reactive",
            "Reporting",
            "Runtime",
            "Scheduling",
            "Scripting",
            "Search",
            "Security",
            "Serialization",
            "Services",
            "Speech",
            "State Management",
            "Static Site Generation",
            "Templates",
            "Templating",
            "Testing",
            "Text Processing",
            "Tool",
            "Type Provider",
            "UWP",
            "Utility",
            "Validation",
            "Versioning",
            "Visual Studio",
            "WASM",
            "WPF",
            "Web Framework",
            "Web Utility",
            "WinForms",
            "XAML",
            "Xamarin",
            "iOS",
            "macOS"
        };

        public ValidateProjectTags()
            : base(nameof(ProjectData))
        {
        }

        protected override void AnalyzeData(IDocument document, IAnalyzerContext context)
        {
            IReadOnlyList<string> tags = document.GetList<string>(SiteKeys.Tags);
            if (tags == null || tags.Count == 0)
            {
                // No topics, but that's okay (if we ever want to enforce at least one topic, add an analyzer result here)
                return;
            }
            string[] nonApprovedTags = tags.Where(x => !Tags.Contains(x)).ToArray();
            if (nonApprovedTags.Length > 0)
            {
                context.AddAnalyzerResult(document, $"Document contains non-approved tag(s): {string.Join(", ", nonApprovedTags)}");
            }
        }
    }
}
