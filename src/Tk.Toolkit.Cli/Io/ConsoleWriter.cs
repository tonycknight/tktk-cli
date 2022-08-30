using Tk.Extensions;

namespace Tk.Toolkit.Cli.Io
{
    internal class ConsoleWriter : IConsoleWriter
    {
        public void Write(string line) => Console.WriteLine(line);

        public void WriteMany(IEnumerable<string> lines)
        {
            var line = lines.Join(Environment.NewLine);
            Console.WriteLine(line);
        }
    }
}
