using System.Collections.Generic;
using DotNetTool.Builder.Models.Validation;

namespace DotNetTool.Builder.Services.Validation
{
    internal sealed class Command(ValidateCommandInfo validateCommandInfo) : IValidateDotNetTool
    {
        public IEnumerable<ValidationResult> Validate(Models.DotNetTool dotNetTool)
        {
            return validateCommandInfo.Validate(dotNetTool.ParameterInfo);
        }
    }
}