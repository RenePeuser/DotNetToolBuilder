using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Pastel;
using Trumpf.Hmi.Extensions;

namespace trumpf.hmi.dotnettool.builder
{
    public class ParameterExpressionParser
    {
        public CliParameterInfo Parse(string paramterExpression, CliParameterInfo lastParamater)
        {
            var splittedExpression = paramterExpression.Split(" ");

            var listArguments = new List<OptionInfo>();
            CliParameterInfo lastCliParameterInfo = null;
            Argument argument = null;

            for (int i = splittedExpression.Length - 1; i >= 0; i--)
            {
                var current = splittedExpression[i];
                if (current.StartsWith("<") && current.EndsWith(">"))
                {
                    Console.WriteLine();
                    Console.WriteLine($"Please enter a description for your argument: '{current}'".AsInput());
                    var description = Console.ReadLine();

                    var name = current.Substring(1, current.Length - 2);
                    var normalizedArgumentName = name.Split('-').Select(s => s.FirstCharToUpper()).Flatten();
                    argument = new Argument(name, description, current, normalizedArgumentName);
                }
                else if (current.Contains("-"))
                {
                    Console.WriteLine();
                    Console.WriteLine($"Please enter an alias for your option: '{current}'".AsInput());
                    var alias = Console.ReadLine();
                    Console.WriteLine();

                    Console.WriteLine();
                    Console.WriteLine($"Is your option required (r) or optional (o): '{current}'".AsInput());
                    var required = Console.ReadLine();
                    var boolRequired = required.ToLower().Equals("r");

                    Console.WriteLine();
                    Console.WriteLine($"Please enter a description for your option: '{current}'".AsInput());
                    var description = Console.ReadLine();

                    var optioName = current.TrimStart('-');
                    var normalizedOptiontName = optioName.Split('-').Select(s => s.FirstCharToUpper()).Flatten();
                    var optionArgumentName = normalizedOptiontName.FirstCharToLower();
                    listArguments.Add(new OptionInfo(current, optioName, alias, description, boolRequired, argument, normalizedOptiontName, optionArgumentName));
                    if (argument.IsNotNull())
                    {
                        argument = null;
                    }
                }
                else
                {
                    var parameter = new CliParameterInfo();
                    parameter.Name = current;

                    Console.WriteLine();
                    Console.WriteLine($"Please enter a description for your command: '{parameter.Name}'".AsInput());
                    var description = Console.ReadLine();
                    parameter.Decsription = description;
                    parameter.Options = listArguments.ToList();
                    listArguments = new List<OptionInfo>();
                    if (lastCliParameterInfo.IsNotNull())
                    {
                        parameter.SubCommands = lastCliParameterInfo.ToIList();
                    }

                    parameter.ArgumentInfo = argument;
                    if (argument.IsNotNull())
                    {
                        argument = null;
                    }

                    lastCliParameterInfo = parameter;

                    if (lastParamater.IsNotNull())
                    {
                        var parentForThis =
                            CliParameterService.FindAlreadyExistingCommand(lastParamater.SubCommands, parameter);
                        if (parentForThis.IsNotNull())
                        {
                            parentForThis.SubCommands = parentForThis.SubCommands.Concat(parameter.SubCommands);
                        }
                    }
                }
            }

            if (lastParamater.IsNotNull())
            {
                return lastParamater;
            }

            return lastCliParameterInfo;
        }
    }
}