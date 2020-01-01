using System.Collections.Generic;
using Argument.Check;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Options
{
    

    public class OptionMethodsBuilder : IOptionMethodsBuilder
    {
        private const string OptionMethodTemplate =
@"        private Option Build$option-name$Option()
        {
            return new $option$;
        }";

        private readonly INewOptionExpressionService _newOptionExpressionService;

        public OptionMethodsBuilder(INewOptionExpressionService newOptionExpressionService)
        {
            Throw.IfNull(() => newOptionExpressionService);

            _newOptionExpressionService = newOptionExpressionService;
        }

        public IEnumerable<MethodInfo> Build(IEnumerable<OptionInfo> options)
        {
            Throw.IfNull(() => options);

            foreach (var option in options)
            {
                var neewOptionStatement = _newOptionExpressionService.Build(option);
                var newMethod = OptionMethodTemplate.Replace("$option$", neewOptionStatement).Replace("$option-name$", option.NormalizedValue);
                var methodName = $"Build{option.NormalizedValue}Option";
                yield return new MethodInfo(methodName, newMethod);
            }
        }
    }
}
