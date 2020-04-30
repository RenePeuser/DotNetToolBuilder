using System.Collections.Generic;
using System.Linq;

namespace DotNetTool.Builder.Services.Validation
{
    internal class DotNetToolValidator
    {
        private readonly IEnumerable<IDotNetToolFromJsonValidator> _validators;

        public DotNetToolValidator(IEnumerable<IDotNetToolFromJsonValidator> validators)
        {
            _validators = validators;
        }

        internal DotNetToolValidationResult Validate(Models.DotNetTool dotNetTool)
        {
            var result = _validators.SelectMany(validator => validator.Validate(dotNetTool));
            return new DotNetToolValidationResult(result);
        }
    }
}
