using Tk.Extensions;

namespace Tk.Toolkit.Cli.Io
{
#pragma warning disable CS0618 // Type or member is obsolete
    internal class ConsoleWriter : IConsoleWriter
#pragma warning restore CS0618 // Type or member is obsolete
    {
        public void Write(string line) => Console.WriteLine(line);

        public void WriteMany(IEnumerable<string> lines)
        {
            var line = lines.Join(Environment.NewLine);
            Console.WriteLine(line);
        }
    }
}
