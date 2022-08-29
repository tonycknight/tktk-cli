using System.IdentityModel.Tokens.Jwt;
using McMaster.Extensions.CommandLineUtils;
using Tk.Extensions;
using Tk.Extensions.Tasks;
using tkdevcli.Io;

namespace tkdevcli.Commands
{
    [Command("decodejwt", Description = "Decode a JWT")]
    internal class DecodeJwtCommand
    {
        private readonly IConsoleWriter _consoleWriter;

        public DecodeJwtCommand(IConsoleWriter consoleWriter)
        {
            _consoleWriter = consoleWriter;
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
                
                var token = Decode(this.Jwt);

                var lines = Format(token).ToTable();
                _consoleWriter.WriteMany(lines);

                return true.ToReturnCode().ToTaskResult();
            }
            catch(Exception ex)
            {
                _consoleWriter.Write(Crayon.Output.Bright.Red($"Invalid JWT.{Environment.NewLine}{ex.Message}"));
                return false.ToReturnCode().ToTaskResult();
            }
        }

        private JwtSecurityToken Decode(string jwt)
        {
            const string prefix = "Bearer ";

            var idx = jwt.IndexOf(prefix);
            jwt = idx >= 0 ? jwt.Substring(prefix.Length) : jwt;

            return new JwtSecurityToken(jwt);
        }

        private IEnumerable<(string, string)> Format(JwtSecurityToken jwt)
        {
            var claims = jwt.Claims.Select(c => (Crayon.Output.Bright.Yellow(c.Type), c.Value))
                                   .ToTable();

            var lines = new (string, string)[]
                {
                    ("Audiences", jwt.Audiences.Join(" ")),
                    ("Actor", jwt.Actor),
                    ("Algorithm", jwt.SignatureAlgorithm),
                    ("Issuer", jwt.Issuer),
                    ("Issued at", jwt.IssuedAt.ToString("yyyy-MM-dd HH:mm:ss")),
                    ("Subject", jwt.Subject),
                    ("Valid from", jwt.ValidFrom.ToString("yyyy-MM-dd HH:mm:ss")),
                    ("Valid to", jwt.ValidTo.ToString("yyyy-MM-dd HH:mm:ss")),
                    ("Claims", Environment.NewLine + claims.Join(Environment.NewLine))
                }
                .Where(t => !string.IsNullOrWhiteSpace(t.Item2))
                .Select(t => (Crayon.Output.Bright.Cyan(t.Item1), t.Item2));
                
            return lines;
        }
    }
}
