namespace DotNetTool.Builder.Services.DotNet
{
    internal interface IDotNetCliArgumentFixer
    {
        string[] Fix(string[] args);
    }
}
