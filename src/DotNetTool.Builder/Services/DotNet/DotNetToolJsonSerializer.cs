using System.IO;
using Argument.Check;
using DotNetTool.Builder.DotNet.Newtool;
using DotNetTool.Builder.ErrorHandling;
using DotNetTool.Builder.ToolBuilder.FromConsole.Services;
using Extensions.Pack;
using FileSystem.Abstraction;
using Newtonsoft.Json;
using FileInfo = System.IO.FileInfo;

namespace DotNetTool.Builder.Services.DotNet
{
    internal class DotNetToolJsonSerializer : IDotNetToolSerializer
    {
        private readonly IConsoleService _consoleService;
        private readonly IFileService _fileService;

        public DotNetToolJsonSerializer(IConsoleService consoleService, IFileService fileService)
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
            return DeserializeFromInternal(file);
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

        private Models.DotNetTool DeserializeFromInternal(IFileInfo fileInfo)
        {
            Throw.IfNull(() => fileInfo);

            if (fileInfo.NotExists)
            {
                throw new DotNetToolBuilderException($"File: '{fileInfo.FullName}' does not exists");
            }

            if (fileInfo.Extension.NotEqualsTo(".json"))
            {
                throw new DotNetToolBuilderException($"File: '{fileInfo.FullName}' must be a json to deserialize to a dot net tool");
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
