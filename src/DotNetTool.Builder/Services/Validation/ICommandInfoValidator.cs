using System.Collections.Generic;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Models.Validation;

namespace DotNetTool.Builder.Services.Validation
{
    internal interface ICommandInfoValidator
    {
        IEnumerable<ValidationResult> Validate(CommandInfo commandInfo);
    }
}