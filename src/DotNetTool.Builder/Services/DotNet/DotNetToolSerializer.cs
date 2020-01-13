using System.IO;
using Argument.Check;
using DotNetTool.Builder.Dotnet.Newtool;
using DotNetTool.Builder.Extensions;
using FileSystem.Abstraction;
using Newtonsoft.Json;
using FileInfo = System.IO.FileInfo;

namespace DotNetTool.Builder.Services.DotNet
{
    internal class DotNetToolSerializer : IDotNetToolSerializer
    {
        private readonly IConsoleService _consoleService;
        private readonly IFileService _fileService;

        public DotNetToolSerializer(IConsoleService consoleService, IFileService fileService)
        {
            Throw.IfNull(() => consoleService);
            Throw.IfNull(() => fileService);

            _consoleService = consoleService;
            _fileService = fileService;
        }

        public Models.DotNetTool DeserializeFrom(FileInfo fileInfo)
        {
            if (fileInfo.IsNull())
            {
                return null;
            }

            if (fileInfo.Exists.IsFalse())
            {
                _consoleService.WriteError($"File: '{fileInfo.FullName}' does not exists");
                return null;
            }

            var file = _fileService.GetFileInfo(fileInfo.FullName);
            return DeserializeFrom(file);
        }

        public void Serialize(Models.DotNetTool dotNetTool, NewToolParameters target)
        {
            Throw.IfNull(() => dotNetTool);
            Throw.IfNull(() => target);
            Throw.IfNull(() => target);

            var targetDirectory = target.SaveToolTo;
            if (targetDirectory.IsNull())
            {
                return;
            }

            if (targetDirectory.Exists.IsFalse())
            {
                target.SaveToolTo.Create();
            }

            var dotnetToolAsJson = JsonConvert.SerializeObject(dotNetTool, Formatting.Indented);
            var dotnetToolFile = _fileService.GetFileInfo(Path.Combine(targetDirectory.FullName, $"{dotNetTool.ProjectName}.json"));
            dotnetToolFile.WriteAllText(dotnetToolAsJson);
        }

        private Models.DotNetTool DeserializeFrom(IFileInfo fileInfo)
        {
            Throw.IfNull(() => fileInfo);

            if (fileInfo.NotExists())
            {
                _consoleService.WriteError($"File: '{fileInfo.FullName}' does not exists");
                return null;
            }

            if (fileInfo.Extension.NotEqualsTo(".json"))
            {
                _consoleService.WriteError($"File: '{fileInfo.FullName}' must be a json to deserialize to a dot net tool");
                return null;
            }

            Models.DotNetTool dotNetTool = null;
            try
            {
                var dotNetToolAsJson = fileInfo.ReadAllText();
                dotNetTool = JsonConvert.DeserializeObject<Models.DotNetTool>(dotNetToolAsJson);
            }
            catch
            {
                _consoleService.WriteError($"Could not deserialize: '{fileInfo.FullName}'");
            }
            finally
            {
                if (dotNetTool.IsNotNull())
                {
                    _consoleService.WriteSuccess($"DotNetTool successfully deserialized from file: '{fileInfo.FullName}'");
                }
            }

            return dotNetTool;
        }
    }
}
