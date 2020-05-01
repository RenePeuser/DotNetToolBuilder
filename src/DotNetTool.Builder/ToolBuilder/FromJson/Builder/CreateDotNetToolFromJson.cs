using System;
using System.Linq;
using DotNetTool.Builder.DotNet.Newtool;
using DotNetTool.Builder.ErrorHandling;
using DotNetTool.Builder.Services.Builders;
using DotNetTool.Builder.Services.DotNet;
using DotNetTool.Builder.Services.Validation;
using DotNetTool.Builder.ToolBuilder.FromJson.Fixer;
using Extensions.Pack;

namespace DotNetTool.Builder.ToolBuilder.FromJson.Builder
{
    internal class CreateDotNetToolFromJson : IBuildDotNetTool
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
                throw new DotNetToolBuilderException($"Deserialized tool: '{newToolParameters.FromFile.FullName}' has following errors: {validationResult.ValidationResults.Select(result => result.Errors).Flatten(Environment.NewLine)}");
            }
            return deserializedTool;
        }

        public bool IsThisBuilderFor(NewToolParameters newToolParameters)
        {
            return newToolParameters.FromFile.IsNotNull() && newToolParameters.FromFile.Extension.EqualsTo(".json");
        }
    }
}