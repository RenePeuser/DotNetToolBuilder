using System.Collections.Generic;
using DotNetTool.Builder.Models.Validation;

namespace DotNetTool.Builder.Services.Validation
{
    internal class ToolName : IValidateDotNetTool
    {
        private readonly IToolNameValidator _toolNameValidator;

        public ToolName(IToolNameValidator toolNameValidator)
        {
            _toolNameValidator = toolNameValidator;
        }

        public IEnumerable<ValidationResult> Validate(Models.DotNetTool dotNetTool)
        {
            yield return _toolNameValidator.Validate(dotNetTool.DotNetToolName.NormalizedName);
        }
    }
}