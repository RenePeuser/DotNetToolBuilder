using System;
using System.Diagnostics;
using System.IO;

namespace trumpf.hmi.dotnettool.builder.Services
{
    public class VisualStudioService
    {
        public void Open(FileInfo solution)
        {
            var programx86Path = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            var vs2019 = Path.Combine(programx86Path,
                @"Microsoft Visual Studio\2019\Enterprise\Common7\IDE\devenv.exe");
            var fileInfo = new FileInfo(vs2019);
            Process.Start(fileInfo.FullName, solution.FullName);
        }
    }
}