using System.Collections.Generic;
using DotNetTool.Builder.Models.Validation;

namespace DotNetTool.Builder.Services.Validation
{
    internal sealed class ProjectNameTool(IProjectNameValidator projectNameValidator) : IValidateDotNetTool
    {
        public IEnumerable<ValidationResult> Validate(Models.DotNetTool dotNetTool)
        {
            yield return projectNameValidator.Validate(dotNetTool.ProjectName);
        }
    }
}