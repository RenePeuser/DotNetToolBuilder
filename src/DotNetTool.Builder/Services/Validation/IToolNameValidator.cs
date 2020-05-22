using DotNetTool.Builder.Models.Validation;

namespace DotNetTool.Builder.Services.Validation
{
    internal interface IToolNameValidator
    {
        ValidationResult Validate(string value, string projectName);
    }
}
