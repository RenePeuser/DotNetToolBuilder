using System;
using System.Linq;
using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Services;
using Trumpf.Hmi.Extensions;

namespace DotNetTool.Builder.Parser.Argument
{
    public class ArgumentOnly : IArgumentOnly
    {
        private readonly IConsoleService _consoleService;

        public ArgumentOnly(IConsoleService consoleService)
        {
            _consoleService = consoleService;
        }

        public bool IsThisParserFor(string value)
        {
            return value.StartsWith("<") && value.EndsWith(">");
        }

        public ArgumentInfo Parse(string value)
        {
            var name = value.Substring(1, value.Length - 2);
            var normalizedArgumentName = name.Split('-').Select(s => s.FirstCharToUpper()).Flatten();
            var typeInfo = "object";

            _consoleService.WriteLine();
            _consoleService.WriteLine($"Please enter a description for your argument: '{value}'".AsInput());
            var description = _consoleService.ReadLine();

            var argument = new ArgumentInfo(name, description, value, normalizedArgumentName, typeInfo);

            return argument;
        }
    }
}