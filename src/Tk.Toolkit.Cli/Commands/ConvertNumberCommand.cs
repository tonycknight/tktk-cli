using McMaster.Extensions.CommandLineUtils;
using Spectre.Console;
using Tk.Toolkit.Cli.Conversions;

namespace Tk.Toolkit.Cli.Commands
{
    [Command("conv", Description = "Convert a number between different bases")]
    internal class ConvertNumberCommand
    {
        private readonly IAnsiConsole _console;
        private readonly INumericValueConverter _converter;

        public ConvertNumberCommand(IAnsiConsole console, INumericValueConverter converter)
        {
            _console = console;
            _converter = converter;
        }

        [Argument(0, Description = "The number to convert.", Name = "value")]
        public string? Value { get; set; }

        public int OnExecute()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Value))
                {
                    _console.Write(new Markup("[red]Missing value.[/]"));
                    return false.ToReturnCode();
                }

                var val = _converter.Parse(Value);

                var results = _converter.Convert(val)
                                        .Select(v => v.Value.ToString())
                                        .ToSpectreList();

                _console.Write(results);

                return true.ToReturnCode();
            }
            catch (Exception)
            {
                _console.Write(new Markup($"[red]Invalid value.[/]"));
                return false.ToReturnCode();
            }
        }
    }
}
