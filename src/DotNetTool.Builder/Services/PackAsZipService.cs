using System;
using System.IO.Compression;
using System.Threading.Tasks;
using DotNetTool.Builder.DotNet.Newtool;
using DotNetTool.Builder.ErrorHandling;
using DotNetTool.Builder.ToolBuilder.FromConsole.Services;
using Extensions.Pack;
using FileSystem.Abstraction;

namespace DotNetTool.Builder.Services
{
    internal sealed class PackAsZipService(IFileService fileService,
                                           IConsoleService consoleService)
    {
        internal void PackAsync(IDirectoryInfo directoryInfo, NewToolParameters newToolParameters)
        {
            if (newToolParameters.TargetZipFileInfo.IsNull())
            {
                return;
            }

            var target = fileService.GetFileInfo(newToolParameters.TargetZipFileInfo.FullName);
            if (target.Directory.NotExists)
            {
                target.Directory.Create();
            }

            try
            {
                ZipFile.CreateFromDirectory(directoryInfo.FullName, target.FullName);
            }
            catch (Exception e)
            {
                throw new DotNetToolBuilderException($"Could not create zip file: {target.FullName}.{Environment.NewLine}{e.Message}");
            }

            consoleService.WriteSuccess($"Zip-File: '{target.FullName}' successfully created.");
        }
    }
}