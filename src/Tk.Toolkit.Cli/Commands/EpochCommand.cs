using McMaster.Extensions.CommandLineUtils;
using Spectre.Console;
using Tk.Extensions.Tasks;

namespace Tk.Toolkit.Cli.Commands
{
    [Command("epoch", Description = "Convert an epoch to date/time and vice-versa")]
    internal class EpochCommand
    {
        private readonly IAnsiConsole _console;

        public EpochCommand(IAnsiConsole console)
        {
            _console = console;
        }

        [Argument(0, Description = "The number or date/time to convert, or `now`.")]
        public string? Value { get; set; }

        public Task<int> OnExecuteAsync()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Value))
                {
                    _console.Write(new Markup("[red]Missing or invalid value.[/]"));
                    return false.ToReturnCode().ToTaskResult();
                }

                if (long.TryParse(Value, out long value1))
                {
                    _console.WriteLine(DateTimeOffset.FromUnixTimeSeconds(value1).ToString("yyyy-MM-dd HH:mm:ss"));
                    return true.ToReturnCode().ToTaskResult();
                }

                if (DateTimeOffset.TryParse(Value, out DateTimeOffset value2))
                {                    
                    _console.WriteLine(value2.ToUnixTimeSeconds().ToString());
                    return true.ToReturnCode().ToTaskResult();
                }

                if(StringComparer.InvariantCultureIgnoreCase.Equals(Value, "now"))
                {
                    _console.WriteLine(DateTimeOffset.Now.ToUnixTimeSeconds().ToString());
                    return true.ToReturnCode().ToTaskResult();
                }

            }
            catch 
            {                
            }

            _console.Write(new Markup("[red]Invalid value.[/]"));
            return false.ToReturnCode().ToTaskResult();
        }
    }
}
