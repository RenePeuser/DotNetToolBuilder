using System;
using System.Linq;
using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Services;
using DotNetTool.Builder.Tokenizer.Tokens;

namespace DotNetTool.Builder.Parser.Argument
{
    internal class ArgumentParser : IArgumentParser
    {
        private readonly IConsoleService _consoleService;

        public ArgumentParser(IConsoleService consoleService)
        {
            _consoleService = consoleService;
        }

        public ArgumentInfo Parse(ArgumentToken value)
        {
            var argumentToken = value.As<ArgumentToken>();
            var argumentValue = argumentToken.Value;

            var start = argumentValue.IndexOf("<", StringComparison.Ordinal) + 1;
            var end = argumentValue.IndexOf(">", StringComparison.Ordinal);
            var name = argumentValue[start..end];

            var normalizedArgumentName = name.Split('-').Select(s => s.FirstCharToUpper()).Flatten();

            var typeInfo = "object";
            if (argumentValue.Contains("["))
            {
                var startIndex = argumentValue.IndexOf("[", StringComparison.Ordinal) + 1;
                var endIndex = argumentValue.IndexOf("]", StringComparison.Ordinal);
                typeInfo = argumentValue[startIndex..endIndex];
            }

            _consoleService.WriteInput($"Please enter a description for your argument: '{argumentValue}'");
            var description = _consoleService.ReadLine();

            var argument = new ArgumentInfo(name, description, argumentValue, normalizedArgumentName, typeInfo);

            return argument;
        }
    }
}
