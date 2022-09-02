using McMaster.Extensions.CommandLineUtils;
using Spectre.Console;
using Tk.Extensions.Tasks;
using Tk.Toolkit.Cli.Jwts;

namespace Tk.Toolkit.Cli.Commands
{
    [Command("decodejwt", Description = "Decode a JWT")]
    internal class DecodeJwtCommand
    {
        private readonly IJwtParser _jwtParser;

        public DecodeJwtCommand(Jwts.IJwtParser jwtParser)
        {
            _jwtParser = jwtParser;
        }

        [Argument(0, Description ="The JWT to decode.")]
        public string? Jwt { get; set; }

        public Task<int> OnExecuteAsync()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Jwt))
                {
                    AnsiConsole.Markup("[red]Missing JWT.[/]");
                    return false.ToReturnCode().ToTaskResult();
                }
                
                var lines = _jwtParser.Parse(this.Jwt)
                                      .Select(t => (t.Item1, t.Item2))
                                      .ToSpectreTable();
                
                AnsiConsole.Write(lines);

                return true.ToReturnCode().ToTaskResult();
            }
            catch (Exception)
            {
                AnsiConsole.Write(new Markup($"[red]Invalid JWT.[/]"));                
                return false.ToReturnCode().ToTaskResult();
            }
        }
    }
}
