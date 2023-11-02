namespace DotNetTool.Builder.Models.Validation
{
    internal sealed class DotNetToolNameValidationResult : GenericValidationResult<DotNetToolName>
    {
        public DotNetToolNameValidationResult(DotNetToolName source, string errors) : base(source, errors)
        {
        }
    }
}