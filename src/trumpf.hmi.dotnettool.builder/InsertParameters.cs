using System;
using System.Drawing;
using Pastel;
using trumpf.hmi.dotnettool.builder.Extensions;
using trumpf.hmi.dotnettool.builder.Models;
using Trumpf.Hmi.Extensions;

namespace trumpf.hmi.dotnettool.builder
{
    public class InsertParameters
    {
        public CliParameterInfo Collect()
        {
            CliParameterInfo parameter = null;

            while (true)
            {
                Console.WriteLine("Please enter your parameter expression".AsInput());
                Console.WriteLine($"Sample: 'dotnet tool install --global <package>  [--version not needed is a default command]')".AsSample());

                var parameterExpression = Console.ReadLine();

                var parseResult = new ParameterExpressionParser().Parse(parameterExpression, parameter);
                if (parameter.IsNull())
                {
                    parameter = parseResult;
                }

                Console.WriteLine();

                Console.WriteLine($"Do you want to add another parameter expression ? yes(y) or no (n)".AsInput());

                var result = Console.ReadLine();
                if (result.Contains("no") || result.Contains("n"))
                {
                    return parameter;
                }
            }
        }
    }
}