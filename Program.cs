using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Statiq.App;
using Statiq.Common;
using Statiq.Web;

namespace DiscoverDotnet
{
    public class Program
    {
        private static async Task<int> Main(string[] args)
        {
            return await Bootstrapper.Factory
                .CreateDefault(args)
                .AddHostingCommands()
                .ConfigureServices(services => services
                    .AddSingleton<GitHubManager>()
                    .AddSingleton<FoundationManager>()
                    .AddSingleton<TotalIssueCounts>())
                .RunAsync();
        }
    }
}