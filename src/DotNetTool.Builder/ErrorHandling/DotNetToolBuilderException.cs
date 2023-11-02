using System;

namespace DotNetTool.Builder.ErrorHandling
{
    internal sealed class DotNetToolBuilderException : Exception
    {
        public DotNetToolBuilderException(string message) : base(message)
        {
        }
    }
}
