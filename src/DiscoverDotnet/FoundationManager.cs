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
    public class FoundationManager
    {
        private const int MaxRetry = 3;

        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        private string _readme = null;

        public async Task PopulateAsync(IExecutionContext context)
        {
            AsyncRetryPolicy<string> retryPolicy = Policy<string>
                .Handle<Exception>()
                .WaitAndRetryAsync(MaxRetry, attempt =>
                {
                    context.LogInformation($"Foundation retry {attempt}");
                    return TimeSpan.FromSeconds(1 * Math.Pow(2, attempt));
                });
            await _semaphore.WaitAsync();
            try
            {
                if (_readme == null)
                {
                    context.LogInformation("Getting .NET Foundation readme");
                    _readme = await retryPolicy.ExecuteAsync(
                        async _ =>
                        {
                            using (HttpClient httpClient = context.CreateHttpClient())
                            {
                                return await httpClient.GetStringAsync("https://raw.githubusercontent.com/dotnet/home/master/README.md");
                            }
                        },
                        context.CancellationToken);
                }
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public bool IsInFoundation(string owner, string name)
        {
            if (_readme == null)
            {
                throw new InvalidOperationException("Foundation data not populated");
            }
            return _readme.Contains($"github.com/{owner}/{name}", StringComparison.OrdinalIgnoreCase);
        }
    }
}
