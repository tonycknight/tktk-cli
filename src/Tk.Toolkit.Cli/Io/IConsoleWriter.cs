namespace Tk.Toolkit.Cli.Io
{
    [Obsolete("Use IAnsiConsole")]
    internal interface IConsoleWriter
    {
        void Write(string line);
        void WriteMany(IEnumerable<string> lines);
    }
}
