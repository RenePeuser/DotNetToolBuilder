using System;

namespace DotNetTool.Builder.ErrorHandling
{
    internal sealed class DotNetToolBuilderException(string message) : Exception(message);
}
