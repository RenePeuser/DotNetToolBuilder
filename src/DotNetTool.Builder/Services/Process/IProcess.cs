using System;
using System.Diagnostics;
using System.IO;

namespace DotNetTool.Builder.Services.Process
{
    internal interface IProcess : IDisposable
    {
        bool EnableRaisingEvents { get; set; }

        ProcessStartInfo StartInfo { get; }

        int ExitCode { get; }

        StreamReader StandardOutput { get; }
        event EventHandler Exited;

        bool Start();
    }
}
