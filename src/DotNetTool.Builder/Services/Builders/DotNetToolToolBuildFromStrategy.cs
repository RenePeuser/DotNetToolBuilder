using System;
using System.Collections.Generic;
using System.Linq;
using DotNetTool.Builder.DotNet.Newtool;
using DotNetTool.Builder.ErrorHandling;
using DotNetTool.Builder.Extensions;
using Extensions.Pack;
using Newtonsoft.Json;

namespace DotNetTool.Builder.Services.Builders
{
    internal sealed class DotNetToolToolBuildFromStrategy(IEnumerable<IBuildDotNetTool> dotNetToolStrategies)
    {
        internal Models.DotNetTool CreateFrom(NewToolParameters newToolParameters)
        {
            var builder = dotNetToolStrategies.SingleOrDefault(strategy => strategy.IsThisBuilderFor(newToolParameters));
            if (builder.IsNull())
            {
                throw new DotNetToolBuilderException($"Could not find strategy for your given parameters: {Environment.NewLine}{Environment.NewLine}{newToolParameters.ToInfo()}");
            }
            return builder.BuildFrom(newToolParameters);
        }
    }
}
