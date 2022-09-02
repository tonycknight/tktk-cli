using Spectre.Console;

namespace Tk.Toolkit.Cli
{
    internal static class Extensions
    {
        public static int ApplyDefault(this int value, Func<int, bool> applyDefault, int defaultValue) 
            => applyDefault(value) ? defaultValue : value;

        public static Table ToSpectreTable(this IEnumerable<(string, string)> keyValues)
        {
            var result = new Table()
                .Border(TableBorder.None)
                .AddColumns("", "");

            foreach(var t in keyValues)
            {
                result.AddRow($"[cyan]{Markup.Escape(t.Item1)}[/]", t.Item2);
            }

            return result;
        }
    }
}
