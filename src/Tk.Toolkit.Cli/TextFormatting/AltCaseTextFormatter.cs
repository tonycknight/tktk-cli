using System.Text;

namespace Tk.Toolkit.Cli.TextFormatting
{
    internal class AltCaseTextFormatter : ITextFormatter
    {
        public string Format(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }
            var sb = new StringBuilder(value.Length);
            bool upper = true;
            foreach (char c in value)
            {
                if (char.IsLetter(c))
                {
                    sb.Append(upper ? char.ToUpper(c) : char.ToLower(c));
                    upper = !upper;
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
    }
}
