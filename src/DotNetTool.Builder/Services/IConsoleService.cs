namespace DotNetTool.Builder.Services
{
    public interface IConsoleService
    {
        void WriteLine();
        void WriteLine(string value);
        string ReadLine();
    }
}