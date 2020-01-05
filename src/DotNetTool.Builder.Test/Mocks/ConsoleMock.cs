using System;
using System.Collections.Generic;
using System.Text;
using DotNetTool.Builder.Services;

namespace DotNetTool.Builder.Test.Mocks
{
    internal class ConsoleMock : IConsoleService
    {
        public string ReadLine()
        {
            return string.Empty;
        }

        public void WriteInput(string value)
        {
        }

        public void WriteSample(string value)
        {
        }

        public void WriteError(string value)
        {
        }

        public void WriteSuccess(string value)
        {
        }

        public void WriteInfo(string value)
        {
        }

        public void WriteLine()
        {
        }
    }
}
