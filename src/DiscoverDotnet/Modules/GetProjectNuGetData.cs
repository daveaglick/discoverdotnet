using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Statiq.Common;

namespace DiscoverDotnet.Modules
{
    public class GetProjectNuGetData : Module
    {
        protected override Task<IEnumerable<IDocument>> ExecuteInputAsync(IDocument input, IExecutionContext context)
        {
            return base.ExecuteInputAsync(input, context);
        }
    }
}
