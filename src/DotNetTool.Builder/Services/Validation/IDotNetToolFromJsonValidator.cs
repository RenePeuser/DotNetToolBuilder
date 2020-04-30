using System.Collections.Generic;
using DotNetTool.Builder.Validation;

namespace DotNetTool.Builder.Services.Validation
{
    internal interface IDotNetToolFromJsonValidator
    {
        IEnumerable<ValidationResult> Validate(Models.DotNetTool dotNetTool);
    }
}