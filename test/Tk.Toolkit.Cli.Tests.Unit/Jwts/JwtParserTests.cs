using System.Linq;
using FsCheck.Xunit;
using Tk.Toolkit.Cli.Jwts;

namespace Tk.Toolkit.Cli.Tests.Unit.Jwts
{
    public class JwtParserTests
    {
        [Property(Verbose = true, Arbitrary = new[] { typeof(JwtStringArbitraries) })]
        public bool Parse_ValidJwt_HasUniqueKeys(string jwt)
        {
            var parser = new JwtParser();

            var result = parser.Parse(jwt)
                .ToDictionary(t => t.Item1, t => t.Item2);

            return result["Issuer"] == JwtStringArbitraries.Issuer;
        }


        [Property(Verbose = true, Arbitrary = new[] { typeof(JwtStringArbitraries) })]
        public bool Parse_ValidJwt_AsBearerToken_HasUniqueKeys(string jwt)
        {
            var token = $"Bearer {jwt}";

            var parser = new JwtParser();

            var result = parser.Parse(token)
                .ToDictionary(t => t.Item1, t => t.Item2);

            return result["Issuer"] == JwtStringArbitraries.Issuer;
        }
    }
}
