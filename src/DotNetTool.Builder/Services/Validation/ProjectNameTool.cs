using System.Collections.Generic;
using DotNetTool.Builder.Models.Validation;

namespace DotNetTool.Builder.Services.Validation
{
    internal sealed class ProjectNameTool : IValidateDotNetTool
    {
        private readonly IProjectNameValidator _projectNameValidator;

        public ProjectNameTool(IProjectNameValidator projectNameValidator)
        {
            _projectNameValidator = projectNameValidator;
        }

        public IEnumerable<ValidationResult> Validate(Models.DotNetTool dotNetTool)
        {
            yield return _projectNameValidator.Validate(dotNetTool.ProjectName);
        }
    }
}