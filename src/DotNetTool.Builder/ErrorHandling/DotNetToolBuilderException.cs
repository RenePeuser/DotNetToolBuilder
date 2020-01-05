using System;

namespace DotNetTool.Builder.ErrorHandling
{
    internal class DotNetToolBuilderException : Exception
    {
        public DotNetToolBuilderException(string message) : base(message)
        {   
        }
    }
}