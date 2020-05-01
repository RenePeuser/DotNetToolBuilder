namespace DotNetTool.Builder.Models.Validation
{
    internal class DotNetToolNameValidationResult : GenericValidationResult<DotNetToolName>
    {
        public DotNetToolNameValidationResult(DotNetToolName source, string errors) : base(source, errors)
        {
        }
    }
}