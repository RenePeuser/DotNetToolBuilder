using System.Collections.Generic;
using DotNetTool.Builder.Validation;

namespace DotNetTool.Builder.Services.Validation
{
    internal class ToolNameFromJsonValidator : IDotNetToolFromJsonValidator
    {
        private readonly IToolNameValidator _toolNameValidator;

        public ToolNameFromJsonValidator(IToolNameValidator toolNameValidator)
        {
            _toolNameValidator = toolNameValidator;
        }

        public IEnumerable<ValidationResult> Validate(Models.DotNetTool dotNetTool)
        {
            yield return _toolNameValidator.Validate(dotNetTool.DotNetToolName.Value);
        }
    }
}