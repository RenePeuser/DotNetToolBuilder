using System.Collections.Generic;
using DotNetTool.Builder.Models.Validation;

namespace DotNetTool.Builder.Services.Validation
{
    internal sealed class ToolName(IToolNameValidator toolNameValidator) : IValidateDotNetTool
    {
        public IEnumerable<ValidationResult> Validate(Models.DotNetTool dotNetTool)
        {
            yield return toolNameValidator.Validate(dotNetTool.DotNetToolName.NormalizedName, dotNetTool.ProjectName);
        }
    }
}