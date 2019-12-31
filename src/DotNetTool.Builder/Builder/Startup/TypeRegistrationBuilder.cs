using System.Collections.Generic;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Startup
{
    using global::Argument.Check;

    public class TypeRegistrationBuilder : ITypeRegistrationBuilder
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
