using System.Collections.Generic;
using System.Threading.Tasks;
using Argument.Check;
using DotNetTool.Builder.Dotnet.Newtool;
using FileSystem.Abstraction;

namespace DotNetTool.Builder.Services.IDE
{
    internal class UseIDE : IUseIDE
    {
        private readonly IEnumerable<ISpecificIDE> _specificIDEs;

        public UseIDE(IEnumerable<ISpecificIDE> specificIdEs)
        {
            Throw.IfNull(() => specificIdEs);

            _specificIDEs = specificIdEs;
        }

        public async Task OpenAsync(IFileInfo solutionFileInfo, NewToolParameters parameters)
        {
            Throw.IfNull(() => solutionFileInfo);
            Throw.IfNull(() => parameters);

            foreach (var ide in _specificIDEs)
            {
                await ide.OpenAsync(solutionFileInfo, parameters).ConfigureAwait(false);
            }
        }
    }
}
