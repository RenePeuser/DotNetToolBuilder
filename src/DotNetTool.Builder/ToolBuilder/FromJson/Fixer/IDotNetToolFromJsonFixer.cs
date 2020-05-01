namespace DotNetTool.Builder.ToolBuilder.FromJson.Fixer
{
    internal interface IDotNetToolFromJsonFixer
    {
        Models.DotNetTool FixMissingValues(Models.DotNetTool dotNetTool);
    }
}