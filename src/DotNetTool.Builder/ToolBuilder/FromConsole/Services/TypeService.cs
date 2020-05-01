using System.Collections.Generic;
using System.Linq;
using DotNetTool.Builder.Services;
using Extensions.Pack;
using FileSystem.Abstraction;

namespace DotNetTool.Builder.ToolBuilder.FromConsole.Services
{
    internal class TypeService : ITypeService
    {
        public string GetFullQualifiedName(string projectName, IFileInfo fileInfo)
        {
            var path = CollectPath(fileInfo.Directory, projectName).Reverse().ToList();
            var fullQualifiedName = $"{projectName}.{path.Flatten(".")}.{fileInfo.NameWithoutExtension}";
            return fullQualifiedName;
        }

        private static IEnumerable<string> CollectPath(IDirectoryInfo startDirectoryInfo, string name)
        {
            if (startDirectoryInfo == null)
            {
                yield break;
            }

            if (startDirectoryInfo.Name != name)
            {
                // Hint: Structure in the solutions all was normalized, that first char is to upper.
                yield return startDirectoryInfo.Name.FirstCharToUpper();
            }
            else
            {
                yield break;
            }

            var result = CollectPath(startDirectoryInfo.Parent, name).ToList();
            foreach (var value in result)
            {
                // Hint: Structure in the solutions all was normalized, that first char is to upper.
                yield return value.FirstCharToUpper();
            }
        }
    }
}
