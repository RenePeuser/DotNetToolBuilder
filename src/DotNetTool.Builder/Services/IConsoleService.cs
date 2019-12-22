namespace DotNetTool.Builder.Services
{
    public interface IConsoleService
    {
        void WriteInput(string value);
        void WriteSample(string value);
        void WriteError(string value);
        void WriteSuccess(string value);

        string ReadLine();
        void WriteInfo(string value);
    }
}
