namespace Tk.Toolkit.Cli.JsonFormatting
{
    internal interface IJsonFormatter
    {
        string Format(string json);
    }

    internal class JsonFormatter : IJsonFormatter
    {
        public string Format(string json)
        {
            // TODO: 
            return json;
        }
    }
}
