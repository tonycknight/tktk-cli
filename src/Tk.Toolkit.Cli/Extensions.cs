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

            foreach (var t in keyValues)
            {
                result.AddRow($"[cyan]{Markup.Escape(t.Item1)}[/]", Markup.Escape(t.Item2));
            }

            return result;
        }

        public static Table ToSpectreList(this IEnumerable<string> values)
        {
            var result = new Table()
                .Border(TableBorder.None)
                .HideHeaders()
                .AddColumns("");

            foreach (var t in values)
            {
                result.AddRow($"{Markup.Escape(t)}");
            }

            return result;
        }


        public static string TrimPrefix(this string value, string prefix)
            => value.StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase)
                ? value.Substring(prefix.Length)
                : value;

        public static string EnsurePrefixed(this string value, string prefix)
            => value.StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase)
                ? value
                : $"{prefix}{value}";

        public static IEnumerable<int> GetUniques(this IEnumerable<int> values)
        {
            var uniques = new HashSet<int>();

            foreach (var x in values)
            {
                if (!uniques.Contains(x))
                {
                    uniques.Add(x);
                    yield return x;
                }
            }
        }

        public static Version ToNormalisedVersion(this string version)
        {
            // Strip out the SHA if present
            var x = version.IndexOf('+');
            if (x >= 0)
            {
                version = version.Substring(0, x);
            }

            return Version.Parse(version);
        }
    }
}
