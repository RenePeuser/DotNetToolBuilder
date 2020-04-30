using System.Collections.Generic;
using System.Linq;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Services.Optimizer;
using Extensions.Pack;

namespace DotNetTool.Builder.Services.Fixer
{
    internal class DotNetToolCommandFixer : IDotNetToolFromJsonFixer
    {
        private readonly IEnumerable<ICommandFixer> _commandFixers;

        public DotNetToolCommandFixer(IEnumerable<ICommandFixer> commandFixers)
        {
            _commandFixers = commandFixers;
        }

        public Models.DotNetTool FixMissingValues(Models.DotNetTool dotNetTool)
        {
            var fixedCommandInfo = FixAll(dotNetTool.ParameterInfo);
            return new Models.DotNetTool(dotNetTool.ProjectName, dotNetTool.DotNetToolName, fixedCommandInfo);
        }

        private CommandInfo FixAll(CommandInfo commandInfo)
        {
            _commandFixers.Aggregate(commandInfo, (command, optimizer) => optimizer.Optimize(command));
            foreach (var commandInfoSubCommand in commandInfo.SubCommands)
            {
                return FixAll(commandInfoSubCommand);
            }

            return commandInfo;
        }
    }

    internal class CommandOptimizer : ICommandFixer
    {
        private readonly IDotNetToolNameNormalizer _dotNetToolNameNormalizer;

        public CommandOptimizer(IDotNetToolNameNormalizer dotNetToolNameNormalizer )
        {
            _dotNetToolNameNormalizer = dotNetToolNameNormalizer;
        }

        public CommandInfo Optimize(CommandInfo commandInfo)
        {
            commandInfo.NormalizedName = _dotNetToolNameNormalizer.Normalize(commandInfo.Name);
            return commandInfo;
        }
    }

    internal class ArgumentOptimizer : ICommandFixer
    {
        private readonly IArgumentTypeOptimizer _argumentTypeOptimizer;

        public ArgumentOptimizer(IArgumentTypeOptimizer argumentTypeOptimizer)
        {
            _argumentTypeOptimizer = argumentTypeOptimizer;
        }

        public CommandInfo Optimize(CommandInfo commandInfo)
        {
            var argument = commandInfo.Argument;
            if (argument.IsNull())
            {
                return commandInfo;
            }

            if (argument.Type.IsNullOrWhiteSpace())
            {
                return commandInfo;
            }

            argument.OptimizedType = _argumentTypeOptimizer.OptimizeType(argument.Type);

            return commandInfo;
        }
    }

    internal class OptionOptimizer : ICommandFixer
    {
        private readonly IArgumentTypeOptimizer _argumentTypeOptimizer;
        private readonly IDotNetToolNameNormalizer _dotNetToolNameNormalizer;

        public OptionOptimizer(IArgumentTypeOptimizer argumentTypeOptimizer, IDotNetToolNameNormalizer dotNetToolNameNormalizer)
        {
            _argumentTypeOptimizer = argumentTypeOptimizer;
            _dotNetToolNameNormalizer = dotNetToolNameNormalizer;
        }

        public CommandInfo Optimize(CommandInfo commandInfo)
        {
            if (commandInfo.Options.IsNullOrEmpty())
            {
                return commandInfo;
            }

            foreach (var option in commandInfo.Options)
            {
                var argument = option.Argument;
                if (argument.IsNull())
                {
                    continue;
                }

                argument.OptimizedType = _argumentTypeOptimizer.OptimizeType(argument.Type);
                option.NormalizedName = option.Name.Split('-').Select(s => s.FirstCharToUpper()).Flatten();
            }

            return commandInfo;
        }
    }



    internal interface ICommandFixer
    {
        CommandInfo Optimize(CommandInfo commandInfo);
    }
}