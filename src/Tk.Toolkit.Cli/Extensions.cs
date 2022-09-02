using Spectre.Console;

namespace Tk.Toolkit.Cli
{
    internal static class Extensions
    {
        public static int ApplyDefault(this int value, Func<int, bool> applyDefault, int defaultValue) 
            => applyDefault(value) ? defaultValue : value;

        public static Table ToSpectreColumns(this IEnumerable<(string, string)> keyValues)
        {
            var result = new Table()
                .Border(TableBorder.None)
                .HideHeaders()
                .AddColumns("", "");

            foreach(var t in keyValues)
            {
                result.AddRow($"[cyan]{Markup.Escape(t.Item1)}[/]", t.Item2);
            }

            return result;
        }

        public static Table ToSpectreList(this IEnumerable<string> keyValues)
        {
            var result = new Table()
                .Border(TableBorder.None)
                .HideHeaders()
                .AddColumns("");

            foreach (var t in keyValues)
            {
                result.AddRow($"{Markup.Escape(t)}");
            }

            return result;
        }
    }
}
