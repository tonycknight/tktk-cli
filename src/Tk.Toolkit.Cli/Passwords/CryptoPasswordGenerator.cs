using System.Security.Cryptography;

namespace Tk.Toolkit.Cli.Passwords
{
    internal class CryptoPasswordGenerator : IPasswordGenerator
    {
        private readonly static char[] _upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        private readonly static char[] _lower = _upper.Select(char.ToLower).ToArray();
        private readonly static char[] _numbers = "0123456789".ToCharArray();
        private readonly static char[] _specialChars = "!?@".ToCharArray();

        public string Generate(int length)
        {
            if (length < 4)
            {
                throw new ArgumentOutOfRangeException("The password length must be at least 4 characters.", (Exception?)null);
            }
            var result = GenerateRandomPassword(length);
            while (!IsComplex(result))
            {
                result = GenerateRandomPassword(length);
            }
            return result;
        }

        private string GenerateRandomPassword(int length)
        {
            var cs = Enumerable.Range(0, length)
                .Select(x => GetRandomCharacter())
                .ToArray();

            return new String(cs);
        }

        private char GetRandomCharacter()
        {
            var length = _lower.Length + _upper.Length + _numbers.Length + _specialChars.Length;

            var idx = RandomNumberGenerator.GetInt32(0, length);

            if (idx < _lower.Length)
            {
                return _lower[idx];
            }
            idx -= _lower.Length;
            if (idx < _upper.Length)
            {
                return _upper[idx];
            }
            idx -= _upper.Length;
            if (idx < _numbers.Length)
            {
                return _numbers[idx];
            }
            idx -= _numbers.Length;
            return _specialChars[idx];
        }

        private bool IsComplex(string password) =>
            password.Any(_upper.Contains)
                && password.Any(_lower.Contains)
                && password.Any(_numbers.Contains)
                && password.Any(_specialChars.Contains);
    }
}
