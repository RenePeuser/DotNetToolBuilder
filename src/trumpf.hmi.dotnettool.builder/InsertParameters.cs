using System;
using System.Drawing;
using Pastel;
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
                Console.WriteLine("Please enter your parameter expression: (Sample: dotnet tool list packages --global)".AsInput() + $"{Environment.NewLine}Sample: 'dotnet tool install --global <package> --version <version>')".AsInput());
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