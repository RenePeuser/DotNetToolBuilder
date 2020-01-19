using System.Collections.Generic;

using DotNetTool.Builder.Models;
using Extensions.Pack;

namespace DotNetTool.Builder.Builder.Parameter
{
    internal class ConstructorArgumentBuilder : IConstructorArgumentBuilder
    {
        public IEnumerable<CtorArgument> Build(CommandInfo parameterInfo)
        {
            var argumentInfo = parameterInfo.Argument;
            if (argumentInfo.IsNotNull())
            {
                yield return new CtorArgument(argumentInfo.OptimizedType, argumentInfo.NormalizedName.FirstCharToLower());
            }

            foreach (var optionInfo in parameterInfo.Options)
            {
                if (optionInfo.Argument.IsNotNull())
                {
                    yield return new CtorArgument(optionInfo.Argument.OptimizedType, optionInfo.NormalizedName.FirstCharToLower());
                }
                else
                {
                    yield return new CtorArgument("bool", optionInfo.NormalizedName.FirstCharToLower());
                }
            }
        }
    }
}
