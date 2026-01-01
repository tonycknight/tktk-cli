using McMaster.Extensions.CommandLineUtils;
using Spectre.Console;
using Tk.Toolkit.Cli.JsonFormatting;

namespace Tk.Toolkit.Cli.Commands
{
    [Command("json", Description = "Json formatting")]
    internal class FormatJsonCommand
    {
        private readonly IAnsiConsole _console;
        private readonly IJsonFormatter _jsonFormatter;

        public FormatJsonCommand(IAnsiConsole console, IJsonFormatter jsonFormatter)
        {
            _console = console;
            _jsonFormatter = jsonFormatter;
        }

        [Argument(0, Description = "The Json to convert.", Name = "value")]
        public string Value { get; set; } = "";

        [Option(CommandOptionType.SingleValue, Description = "Indent the json true/false.", LongName = "indent", ShortName = "i")]
        public bool Indent { get; set; } = true;

        [Option(CommandOptionType.SingleValue, Description = "Colourise the json true/false.", LongName = "colour", ShortName = "c")]
        public bool Colourise { get; set; } = true;

        public int OnExecute()
        {
            try
            {
                var result = _jsonFormatter.Format(Value, Indent, Colourise);

                _console.Write(result);

                return true.ToReturnCode();
            }
            catch (Newtonsoft.Json.JsonReaderException ex)
            {
                _console.Write(new Markup($"[red]Invalid value.[/]"));
                return false.ToReturnCode();
            }
            catch (Newtonsoft.Json.JsonException ex)
            {
                _console.Write(new Markup($"[red]Invalid value.[/]"));
                return false.ToReturnCode();
            }
            catch (Exception ex)
            {
                _console.Write(new Markup($"[red]{ex.Message}[/]"));
                return false.ToReturnCode();
            }

        }
    }
}
