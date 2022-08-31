using System.IdentityModel.Tokens.Jwt;

namespace Tk.Toolkit.Cli.Jwts
{
    internal class JwtParser : IJwtParser
    {
        public IEnumerable<(string, string)> Parse(string jwt)
        {
            var token = Decode(jwt);

            return Format(token);
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
            var claims = jwt.Claims.Select(c => ($"Claim [{c.Type}]", c.Value));
            var audiences = jwt.Audiences.Select(c => ("Audience", c));


            var lines = new (string, string)[]
                {
                    ("Actor", jwt.Actor),
                    ("Algorithm", jwt.SignatureAlgorithm),
                    ("Issuer", jwt.Issuer),
                    ("Issued at", jwt.IssuedAt.ToString("yyyy-MM-dd HH:mm:ss")),
                    ("Subject", jwt.Subject),
                    ("Valid from", jwt.ValidFrom.ToString("yyyy-MM-dd HH:mm:ss")),
                    ("Valid to", jwt.ValidTo.ToString("yyyy-MM-dd HH:mm:ss")),
                }
                .Concat(audiences)
                .Concat(claims)
                .Where(t => !string.IsNullOrWhiteSpace(t.Item2))
                .OrderBy(t => t.Item1);

            return lines;
        }

    }
}
