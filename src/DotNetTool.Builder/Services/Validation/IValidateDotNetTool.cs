using System.Collections.Generic;
using DotNetTool.Builder.Models.Validation;

namespace DotNetTool.Builder.Services.Validation
{
    internal interface IValidateDotNetTool
    {
        IEnumerable<ValidationResult> Validate(Models.DotNetTool dotNetTool);
    }
}