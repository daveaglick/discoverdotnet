using System;
using System.Collections.Generic;
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
    // Queues GitHub requests and ensures they're executed sequentially so we don't trigger the abuse detection mechanisms:
    // "Make requests for a single user or client ID serially. Do not make requests for a single user or client ID concurrently."
    // https://developer.github.com/v3/guides/best-practices-for-integrators/#dealing-with-abuse-rate-limits
    public class GitHubManager
    {
        private const int MaxRetry = 3;

        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        private readonly GitHubClient _gitHub;

        public GitHubManager(IEngineSettings settings)
        {
            _gitHub = new GitHubClient(new ProductHeaderValue(nameof(DiscoverDotnet)))
            {
                Credentials = new Credentials(settings.GetString("DISCOVERDOTNET_GITHUB_TOKEN"))
            };
        }

        public async Task<TResult> GetAsync<TResult>(Func<GitHubClient, Task<TResult>> func, IExecutionContext context, bool retry = true)
        {
            AsyncRetryPolicy<TResult> retryPolicy = null;
            if (retry)
            {
                retryPolicy = Policy<TResult>
                    .Handle<ApiException>()
                    .WaitAndRetryAsync(MaxRetry, attempt =>
                    {
                        context.LogInformation($"GitHub retry {attempt}");
                        return TimeSpan.FromSeconds(1 * Math.Pow(2, attempt));
                    });
            }
            await _semaphore.WaitAsync();
            try
            {
                TResult result = retry
                    ? await retryPolicy.ExecuteAsync(async _ => await func(_gitHub), context.CancellationToken)
                    : await func(_gitHub);

                try
                {
                    MiscellaneousRateLimit rateLimit = await _gitHub.Miscellaneous.GetRateLimits();
                    context.LogInformation($"GitHub rate limit: {rateLimit.Resources.Core.Remaining} remaining");
                }
                catch (Exception)
                {
                    // Eat exceptions when getting rate limits
                }

                return result;
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
