using System;
using System.IO;
using System.Linq;
using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.FileSystemAbstraction;
using DotNetTool.Builder.FileSystemAbstraction.Services;

namespace DotNetTool.Builder.Services
{
    public class VisualStudioService : IVisualStudioService
    {
        private readonly IDirectoryService _directoryService;
        private readonly IProcessService _processService;

        public VisualStudioService(IProcessService processService, IDirectoryService directoryService)
        {
            _processService = processService;
            _directoryService = directoryService;
        }

        public void Open(IFileInfo solution)
        {
            var programx86Path = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            var visualStudio2019Folder =
                _directoryService.GetDirectoryInfo(Path.Combine(programx86Path, @"Microsoft Visual Studio\2019\"));
            var vs2019 = visualStudio2019Folder.EnumerateFiles("devenv.exe", SearchOption.AllDirectories)
                .FirstOrDefault();
            if (vs2019.NotExists())
                throw new InvalidOperationException(
                    $"Can not start visual studio 2019, because did not find any version of visual studio in path: '{visualStudio2019Folder.FullName}'");

            _processService.RunCliCommandAsync(vs2019.FullName, solution.FullName);
        }
    }
}