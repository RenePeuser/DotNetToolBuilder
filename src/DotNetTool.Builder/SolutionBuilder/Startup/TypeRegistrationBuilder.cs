using System.Collections.Generic;
using Argument.Check;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.SolutionBuilder.Startup
{
    internal sealed class TypeRegistrationBuilder : ITypeRegistrationBuilder
    {
        public IEnumerable<string> Build(IEnumerable<TypeToRegister> registrations)
        {
            Throw.IfNull(() => registrations);

            foreach (var typeToRegister in registrations)
            {
                yield return $"            services.AddSingleton<{typeToRegister.InterfaceType}, {typeToRegister.ImplementationType}>();";
            }
        }
    }
}
