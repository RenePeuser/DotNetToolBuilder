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
    internal sealed class CreateDotNetToolFromJson(IDotNetToolSerializer dotNetToolJsonSerializer,
                                                   DotNetToolOptimizer dotNetToolOptimizer,
                                                   DotNetToolValidator dotNetToolValidator) : IBuildDotNetTool
    {
        public Models.DotNetTool BuildFrom(NewToolParameters newToolParameters)
        {
            var deserializedTool = dotNetToolJsonSerializer.DeserializeFrom(newToolParameters.FromFile);
            var optimizedTool = dotNetToolOptimizer.FixMissingValues(deserializedTool);
            var validationResult = dotNetToolValidator.Validate(optimizedTool);
            if (validationResult.HasErrors)
            {
                var errors = validationResult.ValidationResults.Where(result => result.IsValid.IsFalse()).ToList();
                throw new DotNetToolBuilderException($"Deserialized tool: '{newToolParameters.FromFile.FullName}' has following errors: {errors.Select(result => result.Errors).Flatten(Environment.NewLine)}");
            }
            return optimizedTool;
        }

        public bool IsThisBuilderFor(NewToolParameters newToolParameters)
        {
            return newToolParameters.FromFile.IsNotNull() && newToolParameters.FromFile.Extension.EqualsTo(".json");
        }
    }
}