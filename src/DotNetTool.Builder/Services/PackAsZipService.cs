using System;
using System.CommandLine;
using System.IO.Compression;
using DotNetTool.Builder.DotNet.Newtool;
using DotNetTool.Builder.ErrorHandling;
using DotNetTool.Builder.ToolBuilder.FromConsole.Services;
using Extensions.Pack;
using FileSystem.Abstraction;

namespace DotNetTool.Builder.Services
{
    internal class PackAsZipService
    {
        private readonly IFileService _fileService;
        private readonly IConsoleService _consoleService;

        public PackAsZipService(IFileService fileService, IConsoleService consoleService)
        {
            _fileService = fileService;
            _consoleService = consoleService;
        }

        internal IFileInfo Pack(IDirectoryInfo directoryInfo, NewToolParameters newToolParameters, Models.DotNetTool dotNetTool)
        {
            var target = newToolParameters.TargetZipFileInfo.IsNull() ? _fileService.GetFileInfo($"{dotNetTool.ProjectName}.zip") : _fileService.GetFileInfo(newToolParameters.TargetZipFileInfo.FullName);

            try
            {
                ZipFile.CreateFromDirectory(directoryInfo.FullName, target.FullName);
            }
            catch (Exception e)
            {
                throw new DotNetToolBuilderException($"Could not create zip file: {target.FullName}.{Environment.NewLine}{e.Message}");
            }

            _consoleService.WriteSuccess($"Zip-File: '{target.FullName}' successfully created.");

            return target;
        }
    }
}