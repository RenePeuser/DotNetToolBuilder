using System;
using System.Diagnostics;
using System.IO;
using Argument.Check;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Services.Process
{
    internal class ProcessProxy : DisposableBase, IProcess
    {
        private readonly System.Diagnostics.Process _process;

        public ProcessProxy(System.Diagnostics.Process process)
        {
            Throw.IfNull(() => process);

            _process = process;
        }

        public event EventHandler Exited;

        public bool Start()
        {
            return _process.Start();
        }

        public StreamReader StandardOutput => _process.StandardOutput;

        public bool EnableRaisingEvents
        {
            get => _process.EnableRaisingEvents;
            set
            {
                _process.EnableRaisingEvents = value;
                if (value)
                {
                    AttachEvents();
                }
                else
                {
                    DetachEvents();
                }
            }
        }

        public ProcessStartInfo StartInfo => _process.StartInfo;

        public int ExitCode => _process.ExitCode;

        private void DetachEvents()
        {
            _process.Exited -= ProcessExitedEventHandler;
        }

        private void AttachEvents()
        {
            _process.Exited += ProcessExitedEventHandler;
        }

        private void ProcessExitedEventHandler(object sender, EventArgs e)
        {
            Exited?.Invoke(sender, e);
        }

        protected override void DisposeManagedResources()
        {
            DetachEvents();
        }
    }
}
