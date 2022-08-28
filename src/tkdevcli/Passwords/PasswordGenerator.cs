namespace tkdevcli.Passwords
{
    internal class PasswordGenerator : IPasswordGenerator
    {        
        private readonly Random _rng = new Random();
        private readonly char[] _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();

        public string Generate(int length) => GenerateRandomPassword(length);

        private string GenerateRandomPassword(int length)
        {
            var xs = Enumerable.Range(0, length)
                .Select(_ => _rng.Next(0, _chars.Length))
                .Select(x => _chars[x])
                .ToArray();

            return new String(xs);
        }
    }
}
