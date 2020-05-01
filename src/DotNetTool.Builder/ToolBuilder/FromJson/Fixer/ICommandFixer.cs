using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.ToolBuilder.FromJson.Fixer
{
    internal interface ICommandFixer
    {
        CommandInfo Optimize(CommandInfo commandInfo);
    }
}