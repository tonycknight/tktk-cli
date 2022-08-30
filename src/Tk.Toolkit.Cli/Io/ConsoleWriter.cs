using Tk.Extensions;

namespace tkdevcli.Io
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
