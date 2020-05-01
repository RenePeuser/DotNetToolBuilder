namespace DotNetTool.Builder.Models.Validation
{
    internal class ObjectValidationResult : ValidationResult
    {
        public ObjectValidationResult(object source, string errors) : base(errors)
        {
            Source = source;
        }

        public object Source { get; }
    }
}