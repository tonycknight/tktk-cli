namespace Tk.Toolkit.Cli.Waffle
{
    public enum RenderMode
    {
        Text,
        Markdown,
        Html
    }

    internal static class RenderingExtensions
    {
        private static readonly Dictionary<string, string> _markdownTranslation = MarkdownTranslation();
        private static readonly Dictionary<string, string> _textTranslation = TextTranslation();
        private static readonly Dictionary<string, string> _htmlTranslation = HtmlTranslation();

        public static string Render(this string value, RenderMode mode)
        {
            var map = mode.GetRenderMap();
            var result = value;
            foreach(var kvp in map)
            {
                result = result.Replace(kvp.Key, kvp.Value, StringComparison.CurrentCultureIgnoreCase);
            }

            return result;
        }

        private static Dictionary<string, string> GetRenderMap(this RenderMode mode) => mode switch
        {
            RenderMode.Markdown => _markdownTranslation,
            RenderMode.Html => _htmlTranslation,
            _ => _textTranslation
        };

        private static Dictionary<string, string> MarkdownTranslation()
            => new Dictionary<string, string>()
            {
                { "[h1]", "\r\n# " },
                { "[/h1]", "\r\n" },
                { "[h2]", "\r\n## " },
                { "[/h2]", "\r\n" },
                { "[h3]", "\r\n### " },
                { "[/h3]", "\r\n" },
                { "[p]", "\r\n\r\n" },
                { "[ol]", "" },
                { "[ul]", "" },
                { "[/ol]", "" },
                { "[/ul]", "" },
                { "[li]", "\r\n* " },
                { "[i]", "*" },
                { "[/i]", "*" },
                { "[b]", "**" },
                { "[/b]", "**" },
                { "[br]", "\r\n" },
                { "[q]", "*\"" },
                { "[/q]", "*\"\r\n" },
                { "[dt]", "\r\n* " },
                { "[dl]", "\r\n" },
                { "[/dl]", "\r\n" },
                { "[dd]", " " },
            };

        private static Dictionary<string, string> HtmlTranslation()
            => new Dictionary<string, string>()
            {
                { "[h1]", "<h1>" },
                { "[/h1]", "</h1>" },
                { "[h2]", "<h2>" },
                { "[/h2]", "</h2>" },
                { "[h3]", "<h3>" },
                { "[/h3]", "</h3>" },
                { "[p]", "<p>" },
                { "[ol]", "<ol>" },
                { "[ul]", "<ul>" },
                { "[/ol]", "</ol>" },
                { "[/ul]", "</ul>" },
                { "[li]", "<li>" },
                { "[i]", "<i>" },
                { "[/i]", "</i>" },
                { "[b]", "<strong>" },
                { "[/b]", "</strong>" },
                { "[br]", "<br>" },
                { "[q]", "<q>" },
                { "[/q]", "</q><br>" },
                { "[dt]", "<dt>" },
                { "[dl]", "<dl>" },
                { "[/dl]", "</dl>" },
                { "[dd]", "</dd>" },
            };

        private static Dictionary<string, string> TextTranslation()
            => new Dictionary<string, string>()
            {
                { "[h1]", "\r\n" },
                { "[/h1]", "" },
                { "[h2]", "\r\n" },
                { "[/h2]", "" },
                { "[h3]", "\r\n" },
                { "[/h3]", "" },
                { "[p]", "\r\n" },
                { "[ol]", "" },
                { "[ul]", "" },
                { "[/ol]", "" },
                { "[/ul]", "" },
                { "[li]", "\r\n- " },
                { "[i]", "" },
                { "[/i]", "" },
                { "[b]", "" },
                { "[/b]", "" },
                { "[br]", "\r\n" },
                { "[q]", "\"" },
                { "[/q]", "\"\r\n" },
                { "[dt]", "\r\n* " },
                { "[dl]", "\r\n" },
                { "[/dl]", "\r\n" },
                { "[dd]", "\r\n" },
            };
    }

}
