using System.Collections.Generic;
using System.Linq;

namespace DotNetTool.Builder.Services.Validation
{
    internal sealed class DotNetToolValidator(IEnumerable<IValidateDotNetTool> validators)
    {
        internal DotNetToolValidationResult Validate(Models.DotNetTool dotNetTool)
        {
            var result = validators.SelectMany(validator => validator.Validate(dotNetTool)).ToList();
            return new DotNetToolValidationResult(result);
        }
    }
}
