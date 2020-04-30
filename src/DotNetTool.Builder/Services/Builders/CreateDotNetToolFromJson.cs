using DotNetTool.Builder.Dotnet.Newtool;
using DotNetTool.Builder.ErrorHandling;
using DotNetTool.Builder.Services.DotNet;
using DotNetTool.Builder.Services.Fixer;
using DotNetTool.Builder.Services.Validation;
using Extensions.Pack;

namespace DotNetTool.Builder.Services.Builders
{
    internal class CreateDotNetToolFromJson : IBuildDotNetToolStrategy
    {
        private readonly IDotNetToolSerializer _dotNetToolJsonSerializer;
        private readonly DotNetToolFromJsonFixer _dotNetToolFromJsonFixer;
        private readonly DotNetToolValidator _dotNetToolValidator;

        public CreateDotNetToolFromJson(IDotNetToolSerializer dotNetToolJsonSerializer, DotNetToolFromJsonFixer dotNetToolFromJsonFixer, DotNetToolValidator dotNetToolValidator)
        {
            _dotNetToolJsonSerializer = dotNetToolJsonSerializer;
            _dotNetToolFromJsonFixer = dotNetToolFromJsonFixer;
            _dotNetToolValidator = dotNetToolValidator;
        }

        public Models.DotNetTool BuildFrom(NewToolParameters newToolParameters)
        {
            var deserializedTool = _dotNetToolJsonSerializer.DeserializeFrom(newToolParameters.FromFile);
            var optimizedTool = _dotNetToolFromJsonFixer.FixMissingValues(deserializedTool);
            var validationResult = _dotNetToolValidator.Validate(optimizedTool);
            if (validationResult.HasErrors)
            {
                throw new DotNetToolBuilderException($"Deserialized tool: '{newToolParameters.FromFile.FullName}' has following errors: .....");
            }
            return deserializedTool;
        }

        public bool IsThisBuilderFor(NewToolParameters newToolParameters)
        {
            return newToolParameters.FromFile.IsNotNull() && newToolParameters.FromFile.Extension.EqualsTo(".json");
        }
    }
}