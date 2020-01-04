using System.Collections.Generic;
using System.Linq;
using DotNetTool.Builder.Extensions;
using FileSystem.Abstraction;

namespace DotNetTool.Builder.Services
{
    public class TypeService : ITypeService
    {
        public string GetFullqualifiedName(string projectName, IFileInfo fileInfo)
        {
            var path = CollectPath(fileInfo.Directory, projectName).Reverse().ToList();
            var fullQualifiedName = $"{projectName}.{path.Flatten(".")}.{fileInfo.FileNameWithoutExtension()}";
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
                yield return startDirectoryInfo.Name;
            }
            else
            {
                yield break;
            }

            var result = CollectPath(startDirectoryInfo.Parent, name).ToList();
            foreach (var value in result)
            {
                yield return value;
            }
        }
    }
}