using DotNetTool.Builder.Models.Validation;

namespace DotNetTool.Builder.Services.Validation
{
    internal interface IInputValidator
    {
        ValidationResult Validate(string value);
    }
}
