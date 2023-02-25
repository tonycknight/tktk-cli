using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using FsCheck;

namespace Tk.Toolkit.Cli.Tests.Unit.Jwts
{
    internal static class JwtStringArbitraries
    {
        public const string Issuer = "arbitrary";

        public static Arbitrary<string> GetArbitrary()
        {
            var handler = new JwtSecurityTokenHandler();

            return Arb.Generate<Guid>()
                       .Select(g => handler.CreateJwtSecurityToken(audience: g.ToString(), issuer: Issuer))
                       .Select(jwt => handler.WriteToken(jwt))
                       .ToArbitrary();
        }
    }
}
