namespace DotNetTool.Builder.Services
{
    internal interface IDotNetCliArgumentFixer
    {
        string[] Fix(string[] args);
    }
}