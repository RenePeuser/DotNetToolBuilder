namespace DotNetTool.Builder.Services.Fixer
{
    internal interface IDotNetToolFromJsonFixer
    {
        Models.DotNetTool FixMissingValues(Models.DotNetTool dotNetTool);
    }
}