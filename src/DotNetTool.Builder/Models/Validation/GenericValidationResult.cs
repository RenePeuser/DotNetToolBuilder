using Extensions.Pack;

namespace DotNetTool.Builder.Models.Validation
{
    internal class GenericValidationResult<T> : ObjectValidationResult
    {
        public GenericValidationResult(T source, string errors) : base(source, errors)
        {
        }

        public T Object => Source.Cast<T>();
    }
}