using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Statiq.App;

namespace DiscoverDotnet
{
    public class Program
    {
        private static async Task<int> Main(string[] args)
        {
            return await Bootstrapper
                .CreateDefault(args)
                .ConfigureServices(services => services
                    .AddSingleton<GitHubManager>()
                    .AddSingleton<FoundationManager>())
                .RunAsync();
        }
    }
}