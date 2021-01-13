using Statiq.Common;
using Statiq.Core;
using Statiq.Sass;

namespace DiscoverDotnet.Pipelines
{
    public class Sass : Pipeline
    {
        public Sass()
        {
            Isolated = true;

            InputModules = new ModuleList
            {
                new ReadFiles("**/{!_,}*.scss")
            };

            PostProcessModules = new ModuleList
            {
                new CompileSass().WithCompactOutputStyle()
            };

            OutputModules = new ModuleList
            {
                new WriteFiles()
            };
        }
    }
}
