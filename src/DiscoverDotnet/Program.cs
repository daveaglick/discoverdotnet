using System.Linq;
using System.Threading.Tasks;
using Statiq.App;

namespace DiscoverDotnet
{
    public class Program
    {
        private static async Task<int> Main(string[] args)
        {
            return await Bootstrapper
                .CreateDefault(args)
                .RunAsync();
        }
    }
}