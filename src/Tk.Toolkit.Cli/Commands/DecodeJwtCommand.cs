using McMaster.Extensions.CommandLineUtils;
using Spectre.Console;
using Tk.Extensions.Tasks;
using Tk.Toolkit.Cli.Jwts;

namespace Tk.Toolkit.Cli.Commands
{
    [Command("jwt", Description = "Decode a JWT")]
    internal class DecodeJwtCommand
    {
        private readonly IAnsiConsole _console;
        private readonly IJwtParser _jwtParser;

        public DecodeJwtCommand(IAnsiConsole console, Jwts.IJwtParser jwtParser)
        {
            _console = console;
            _jwtParser = jwtParser;
        }

        [Argument(0, Description ="The JWT to decode.")]
        public string? Jwt { get; set; }

        public int OnExecute()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Jwt))
                {
                    _console.Write(new Markup("[red]Missing JWT.[/]"));
                    return false.ToReturnCode();
                }
                
                var lines = _jwtParser.Parse(this.Jwt)
                                      .Select(t => (t.Item1, t.Item2))
                                      .ToSpectreColumns();

                _console.Write(lines);

                return true.ToReturnCode();
            }
            catch (Exception)
            {
                _console.Write(new Markup($"[red]Invalid JWT.[/]"));
                return false.ToReturnCode();
            }
        }
    }
}
