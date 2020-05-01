using System;
using System.Linq;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Services.Optimizer;
using DotNetTool.Builder.ToolBuilder.FromConsole.InfoCollectors;
using DotNetTool.Builder.ToolBuilder.FromConsole.Tokenizer.Tokens;
using Extensions.Pack;

namespace DotNetTool.Builder.ToolBuilder.FromConsole.Parser.Argument
{
    internal class ArgumentParser : IArgumentParser
    {
        private readonly IArgumentTypeOptimizer _argumentTypeOptimizer;
        private readonly ICollectDescription _collectDescription;

        public ArgumentParser(IArgumentTypeOptimizer argumentTypeOptimizer, ICollectDescription collectDescription)
        {
            _argumentTypeOptimizer = argumentTypeOptimizer;
            _collectDescription = collectDescription;
        }

        public ArgumentInfo Parse(ArgumentToken value)
        {
            var argumentToken = value.As<ArgumentToken>();
            var argumentValue = argumentToken.Value;

            var start = argumentValue.IndexOf("<", StringComparison.Ordinal) + 1;
            var end = argumentValue.IndexOf(">", StringComparison.Ordinal);
            var name = argumentValue[start..end];

            var typeInfo = "object";
            if (argumentValue.Contains("["))
            {
                var startIndex = argumentValue.IndexOf("[", StringComparison.Ordinal) + 1;
                var endIndex = argumentValue.IndexOf("]", StringComparison.Ordinal);
                typeInfo = argumentValue[startIndex..endIndex];
            }

            var description = _collectDescription.Collect($"Please enter a description for your argument: '{argumentValue}'");
            var optmmizedTypeInfo = _argumentTypeOptimizer.OptimizeType(typeInfo);
            var normalizedName = name.Split('-').Select(s => s.FirstCharToUpper()).Flatten();

            var argument = new ArgumentInfo(name, description, argumentValue, typeInfo, optmmizedTypeInfo, normalizedName);
            return argument;
        }
    }
}
