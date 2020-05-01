using System.Text;
using Argument.Check;
using DotNetTool.Builder.DotNet.Newtool;

namespace DotNetTool.Builder.Extensions
{
    internal static class NewToolParameterExtensions
    {
        internal static string ToInfo(this NewToolParameters newToolParameters)
        {
            Throw.IfNull(() => newToolParameters);

            var stringBuilder= new StringBuilder();
            stringBuilder.AppendLine($"{nameof(newToolParameters.FromFile)} = {newToolParameters?.FromFile?.FullName}");
            stringBuilder.AppendLine($"{nameof(newToolParameters.SaveToolTo)} = {newToolParameters?.SaveToolTo}");
            return stringBuilder.ToString();
        }
    }
}
