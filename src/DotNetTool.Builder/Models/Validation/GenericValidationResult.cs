using Extensions.Pack;

namespace DotNetTool.Builder.Models.Validation
{
    internal class GenericValidationResult<T> : ObjectValidationResult
    {
        internal GenericValidationResult(T source, string errors) : base(source, errors)
        {
        }

        internal T Object => Source.Cast<T>();
    }
}