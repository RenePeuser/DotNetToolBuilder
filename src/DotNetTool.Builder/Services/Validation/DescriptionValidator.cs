using System.Collections.Generic;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Validation;

namespace DotNetTool.Builder.Services.Validation
{
    internal class DescriptionValidator : ICommandInfoValidator
    {
        public IEnumerable<ValidationResult> Validate(CommandInfo commandInfo)
        {
            // ToDo: not important right now
            yield break;
        }
    }
}