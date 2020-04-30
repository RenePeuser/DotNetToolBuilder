using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Services.Fixer
{
    internal interface ICommandFixer
    {
        CommandInfo Optimize(CommandInfo commandInfo);
    }
}