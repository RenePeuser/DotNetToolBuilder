using System.Collections.Generic;
using DotNetTool.Builder.Models.Validation;

namespace DotNetTool.Builder.Services.Validation
{
    internal sealed class Command : IValidateDotNetTool
    {
        private readonly ValidateCommandInfo _validateCommandInfo;

        public Command(ValidateCommandInfo validateCommandInfo)
        {
            _validateCommandInfo = validateCommandInfo;
        }

        public IEnumerable<ValidationResult> Validate(Models.DotNetTool dotNetTool)
        {
            return _validateCommandInfo.Validate(dotNetTool.ParameterInfo);
        }
    }
}