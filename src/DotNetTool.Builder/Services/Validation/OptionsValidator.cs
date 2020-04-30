using System.Collections.Generic;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Validation;
using Extensions.Pack;

namespace DotNetTool.Builder.Services.Validation
{
    internal class OptionsValidator : ICommandInfoValidator
    {
        private readonly ArgumentInfoValidator _argumentInfoValidator;

        public OptionsValidator(ArgumentInfoValidator  argumentInfoValidator)
        {
            _argumentInfoValidator = argumentInfoValidator;
        }

        public IEnumerable<ValidationResult> Validate(CommandInfo commandInfo)
        {
            foreach (var optionInfo in commandInfo.Options)
            {
                var argument = optionInfo.Argument;
                if (argument.IsNotNull())
                {
                }
            }
        }
    }
}