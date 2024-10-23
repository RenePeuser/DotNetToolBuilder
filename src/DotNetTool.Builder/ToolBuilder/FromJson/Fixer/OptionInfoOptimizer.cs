using System.Linq;
using DotNetTool.Builder.Models;
using Extensions.Pack;

namespace DotNetTool.Builder.ToolBuilder.FromJson.Fixer
{
    internal sealed class OptionInfoOptimizer(ArgumentInfoOptimizer argumentInfoOptimizer)
    {
        public OptionInfo Optimize(OptionInfo optionInfo)
        {
            if (optionInfo.IsNull())
            {
                return optionInfo;
            }

            argumentInfoOptimizer.Optimize(optionInfo.Argument);

            optionInfo.NormalizedName = optionInfo.Name.Split('-').Select(s => s.FirstCharToUpper()).Flatten();
            
            return optionInfo;
        }
    }
}