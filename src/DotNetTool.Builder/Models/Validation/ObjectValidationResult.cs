namespace DotNetTool.Builder.Models.Validation
{
    internal class ObjectValidationResult : ValidationResult
    {
        internal ObjectValidationResult(object source, string errors) : base(errors)
        {
            Source = source;
        }

        public object Source { get; }
    }
}