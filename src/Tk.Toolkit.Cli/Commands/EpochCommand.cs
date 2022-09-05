using System;
using McMaster.Extensions.CommandLineUtils;
using Spectre.Console;
using Tk.Extensions.Tasks;
using Tk.Toolkit.Cli.Conversions;

namespace Tk.Toolkit.Cli.Commands
{
    [Command("epoch", Description = "Convert an epoch to date/time")]
    internal class EpochCommand
    {
        private readonly IAnsiConsole _console;

        public EpochCommand(IAnsiConsole console)
        {
            _console = console;
        }

        [Argument(0, Description = "The number to convert.")]
        public string? Value { get; set; }

        public Task<int> OnExecuteAsync()
        {
            try
            {
                long value;
                if (string.IsNullOrWhiteSpace(Value) ||
                    !long.TryParse(Value, out value))
                {
                    _console.Write(new Markup("[red]Missing or invalid value.[/]"));
                    return false.ToReturnCode().ToTaskResult();
                }

                var date = DateTimeOffset.FromUnixTimeSeconds(value);

                _console.WriteLine(date.ToString("yyyy-MM-dd HH:mm:ss"));

                return true.ToReturnCode().ToTaskResult();
            }
            catch (Exception)
            {
                _console.Write(new Markup($"[red]Invalid value.[/]"));
                return false.ToReturnCode().ToTaskResult();
            }
        }
    }
}
