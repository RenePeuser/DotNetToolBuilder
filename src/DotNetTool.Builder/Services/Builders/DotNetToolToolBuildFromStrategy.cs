using System;
using System.Collections.Generic;
using System.Linq;
using DotNetTool.Builder.Dotnet.Newtool;
using DotNetTool.Builder.ErrorHandling;
using DotNetTool.Builder.Extensions;
using Extensions.Pack;
using Newtonsoft.Json;

namespace DotNetTool.Builder.Services.Builders
{
    internal class DotNetToolToolBuildFromStrategy
    {
        private readonly IEnumerable<IBuildDotNetToolStrategy> _dotNetToolStrategies;

        public DotNetToolToolBuildFromStrategy(IEnumerable<IBuildDotNetToolStrategy> dotNetToolStrategies)
        {
            _dotNetToolStrategies = dotNetToolStrategies;
        }
        internal Models.DotNetTool CreateFrom(NewToolParameters newToolParameters)
        {
            var builder = _dotNetToolStrategies.SingleOrDefault(strategy => strategy.IsThisBuilderFor(newToolParameters));
            if (builder.IsNull())
            {
                throw new DotNetToolBuilderException($"Could not find strategy for your given parameters: {Environment.NewLine}{Environment.NewLine}{newToolParameters.ToInfo()}");
            }
            return builder.BuildFrom(newToolParameters);
        }
    }
}
