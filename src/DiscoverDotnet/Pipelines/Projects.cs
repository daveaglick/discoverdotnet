using Statiq.Common;
using Statiq.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscoverDotnet.Pipelines
{
    public class Projects : Pipeline
    {
        public Projects()
        {
            InputModules = new ModuleList
            {
                new ReadFiles(Config.FromContext(x => (IEnumerable<string>)new string[]
                {
                    x.FileSystem.RootPath.Combine("../../data/projects/*.yml").FullPath }
                ))
            };
        }
    }
}
