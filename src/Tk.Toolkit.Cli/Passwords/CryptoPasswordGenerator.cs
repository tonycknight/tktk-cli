using System.Security.Cryptography;

namespace tkdevcli.Passwords
{
    internal class CryptoPasswordGenerator : IPasswordGenerator
    {
        private readonly char[] _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();

        public string Generate(int length) => GenerateRandomPassword(length);

        private string GenerateRandomPassword(int length)
        {            
            var cs = Enumerable.Range(0, length)
                .Select(_ => RandomNumberGenerator.GetInt32(0, _chars.Length))
                .Select(x => _chars[x])
                .ToArray();

            return new String(cs);
        }
    }
}
