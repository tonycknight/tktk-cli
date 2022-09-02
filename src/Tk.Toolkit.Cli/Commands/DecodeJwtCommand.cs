using McMaster.Extensions.CommandLineUtils;
using Tk.Extensions.Tasks;
using Tk.Toolkit.Cli.Io;
using Tk.Toolkit.Cli.Jwts;

namespace Tk.Toolkit.Cli.Commands
{
    [Command("decodejwt", Description = "Decode a JWT")]
    internal class DecodeJwtCommand
    {
        private readonly IConsoleWriter _consoleWriter;
        private readonly IJwtParser _jwtParser;

        public DecodeJwtCommand(IConsoleWriter consoleWriter, Jwts.IJwtParser jwtParser)
        {
            _consoleWriter = consoleWriter;
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
                    _consoleWriter.Write(Crayon.Output.Bright.Red("Missing JWT."));
                    return false.ToReturnCode().ToTaskResult();
                }
                
                var lines = _jwtParser.Parse(this.Jwt)
                                      .Select(t => (Crayon.Output.Bright.Cyan(t.Item1), t.Item2))
                                      .ToTable();

                _consoleWriter.WriteMany(lines);

                return true.ToReturnCode().ToTaskResult();
            }
            catch(Exception ex)
            {
                _consoleWriter.Write(Crayon.Output.Bright.Red($"Invalid JWT.{Environment.NewLine}{ex.Message}"));
                return false.ToReturnCode().ToTaskResult();
            }
        }
    }
}
