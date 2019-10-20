using System.Collections.Generic;
using System.Linq;
using Statiq.Common;
using Statiq.Core;
using Statiq.Json;
using Statiq.Minification;
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

            TransformModules = new ModuleList
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
