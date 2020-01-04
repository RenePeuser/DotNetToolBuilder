using FileSystem.Abstraction;

namespace DotNetTool.Builder.Services
{
    using System.IO;
    using Argument.Check;
    using Extensions;
    using Models;
    using Newtonsoft.Json;

    public class DotNetToolSerializer : IDotNetToolSerializer
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

        public DotNetTool DeserializeFrom(string fileOrFilePath)
        {
            if (fileOrFilePath.IsNullOrWhiteSpace())
            {
                return null;
            }

            if (Path.IsPathFullyQualified(fileOrFilePath).IsFalse())
            {
                _consoleService.WriteError($"File path: '{fileOrFilePath}' is not valid");
                return null;
            }

            var file = _fileService.GetFileInfo(fileOrFilePath);
            return DeserializeFrom(file);
        }

        public DotNetTool DeserializeFrom(FileInfo fileInfo)
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

        public DotNetTool DeserializeFrom(IFileInfo fileInfo)
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

            DotNetTool dotNetTool = null;
            try
            {
                var dotNetToolAsJson = fileInfo.ReadAllText();
                dotNetTool = JsonConvert.DeserializeObject<DotNetTool>(dotNetToolAsJson);
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