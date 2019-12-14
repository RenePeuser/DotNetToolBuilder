using System.Collections.Generic;
using Trumpf.Hmi.Extensions;

namespace trumpf.hmi.dotnettool.builder
{
    public class CliParameterService
    {
        public static CliParameterInfo FindAlreadyExistingCommand(IEnumerable<CliParameterInfo> others,
            CliParameterInfo current)
        {
            if (others.IsNull())
            {
                return null;
            }

            foreach (var cliParameterInfo in others)
            {
                if (cliParameterInfo.Name == current.Name)
                {
                    return cliParameterInfo;
                }

                var match = FindAlreadyExistingCommand(cliParameterInfo.SubCommands, current);
                if (match.IsNotNull())
                {
                    return match;
                }
            }

            return null;
        }
    }
}