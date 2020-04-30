using System.Collections.Generic;
using DotNetTool.Builder.Validation;

namespace DotNetTool.Builder.Services.Validation
{
    internal class ProjectNameToolFromJsonValidator : IDotNetToolFromJsonValidator
    {
        private readonly IProjectNameValidator _projectNameValidator;

        public ProjectNameToolFromJsonValidator(IProjectNameValidator projectNameValidator)
        {
            _projectNameValidator = projectNameValidator;
        }

        public IEnumerable<ValidationResult> Validate(Models.DotNetTool dotNetTool)
        {
            yield return _projectNameValidator.Validate(dotNetTool.ProjectName);
        }
    }
}