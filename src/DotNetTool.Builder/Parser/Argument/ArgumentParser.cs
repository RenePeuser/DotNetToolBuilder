using System;
using System.Collections.Generic;
using System.IO;
using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Services;
using DotNetTool.Builder.Services.Optimizer;
using DotNetTool.Builder.Tokenizer.Tokens;

namespace DotNetTool.Builder.Parser.Argument
{
    internal class ArgumentParser : IArgumentParser
    {
        private readonly IConsoleService _consoleService;
        private readonly IArgumentTypeOptimizer _argumentTypeOptimizer;

        public ArgumentParser(IConsoleService consoleService, IArgumentTypeOptimizer argumentTypeOptimizer)
        {
            _consoleService = consoleService;
            _argumentTypeOptimizer = argumentTypeOptimizer;
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

            _consoleService.WriteInput($"Please enter a description for your argument: '{argumentValue}'");
            var description = _consoleService.ReadLine();

            var optmmizedTypeInfo = _argumentTypeOptimizer.OptimizeType(typeInfo);

            var argument = new ArgumentInfo(name, description, argumentValue, typeInfo, optmmizedTypeInfo);

            return argument;
        }
    }
}
