using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Tk.Toolkit.Cli.JsonFormatting
{
    internal interface IJsonFormatter
    {
        string Format(string json, bool indent);
    }

    internal class JsonFormatter : IJsonFormatter
    {
        public string Format(string json, bool indent)
        {
            var settings = CreateSettings(indent);

            var x = JsonConvert.DeserializeObject(json, settings);

            return JsonConvert.SerializeObject(x, settings);
        }

        private JsonSerializerSettings CreateSettings(bool indent)
        {
            var converters = new List<JsonConverter>
                {
                    new StringEnumConverter()
                };

            return new JsonSerializerSettings
            {
                Formatting = indent ? Formatting.Indented : Formatting.None,
                Converters = converters
            };
        }
    }
}
