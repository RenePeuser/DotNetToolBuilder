namespace Trumpf.Hmi.Uif.List.Dependencies.Services
{
    using YetAnotherConsoleTables;

    internal class MyFormatter : ConsoleTableFormat
    {
        public MyFormatter() : base(' ', ' ', ' ', ' ', Borders.Left | Borders.Right | Borders.HeaderDelimiter)
        {
        }
    }
}