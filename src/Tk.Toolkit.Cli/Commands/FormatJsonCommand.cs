using McMaster.Extensions.CommandLineUtils;
using Spectre.Console;

namespace Tk.Toolkit.Cli.Commands
{
    [Command("json", Description = "Json formatting")]
    internal class FormatJsonCommand
    {
        private readonly IAnsiConsole _console;

        public FormatJsonCommand(IAnsiConsole console)
        {
            _console = console;
        }

        [Argument(0, Description = "The Json to convert.", Name = "value")]
        public string? Value { get; set; }

        // TODO: options? colouring? indentation?

        public int OnExecute()
        {
            try
            {
                // TODO: 
                return true.ToReturnCode();
            }
            catch (Exception ex)
            {
                _console.Write(new Markup($"[red]Invalid value.[/]"));
                return false.ToReturnCode();
            }
        }
    }
}
