using System;
using System.Diagnostics;
using System.IO;

namespace trumpf.hmi.dotnettool.builder.Services
{
    public interface IProcess : IDisposable
    {
        event EventHandler Exited;

        bool EnableRaisingEvents { get; set; }

        ProcessStartInfo StartInfo { get; }

        int ExitCode { get; }

        bool Start();

        StreamReader StandardOutput { get; }
    }
}