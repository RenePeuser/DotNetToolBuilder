using System;

namespace DotNetTool.Builder.ErrorHandling
{
    public class DotNetToolBuilderException : Exception
    {
        public DotNetToolBuilderException(string message) : base(message)
        {   
        }
    }
}