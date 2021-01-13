using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Octokit;
using Polly;
using Polly.Retry;
using Statiq.Common;

namespace DiscoverDotnet
{
    public class TotalIssueCounts
    {
        public int All { get; set; }
        public int HelpWanted { get; set; }
        public int Microsoft { get; set; }
        public int Foundation { get; set; }
        public int Platform { get; set; }
    }
}
