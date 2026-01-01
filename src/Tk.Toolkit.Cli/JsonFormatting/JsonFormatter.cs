using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Tk.Toolkit.Cli.JsonFormatting
{
    internal interface IJsonFormatter
    {
        string Format(string json, bool indent, bool colourise);
    }

    internal class JsonFormatter : IJsonFormatter
    {
        public string Format(string json, bool indent, bool colourise)
        {
            var settings = CreateSettings(indent, colourise);

            var x = JsonConvert.DeserializeObject(json, settings);

            return JsonConvert.SerializeObject(x, settings); 
        }

        private JsonSerializerSettings CreateSettings(bool indent, bool colourise)
        {
            return new JsonSerializerSettings
            {
                Formatting = indent ? Formatting.Indented : Formatting.None,
                Converters = new List<JsonConverter>
                {
                    new StringEnumConverter()
                }
            };
        }
    }
}
