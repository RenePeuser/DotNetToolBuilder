using System.Collections.Generic;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Startup
{
    public interface ITypeRegistrationBuilder
    {
        IEnumerable<string> Build(IEnumerable<TypeToRegister> registrations);
    }
}
