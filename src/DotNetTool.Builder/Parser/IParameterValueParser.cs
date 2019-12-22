namespace DotNetTool.Builder.Parser
{
    public interface IParameterValueParser
    {
        bool IsThisParserFor(string value);
    }
}
