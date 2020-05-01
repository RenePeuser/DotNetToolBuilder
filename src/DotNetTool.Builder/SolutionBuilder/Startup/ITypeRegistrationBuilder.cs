using System.Collections.Generic;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.SolutionBuilder.Startup
{
    internal interface ITypeRegistrationBuilder
    {
        IEnumerable<string> Build(IEnumerable<TypeToRegister> registrations);
    }
}
