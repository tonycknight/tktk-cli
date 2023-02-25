using System.Security.Cryptography;

namespace Tk.Toolkit.Cli.Usernames
{
    internal class UsernameGenerator : IUsernameGenerator
    {
        private readonly IRng _rng;
        private readonly IWordProvider _words;

        public UsernameGenerator(IRng rng, IWordProvider words)
        {
            _rng = rng;
            _words = words;
        }

        public string Generate() => GenerateRandomUsername();

        private string GenerateRandomUsername()
        {
            var noun = PickWord(_words.GetNouns());
            var adj = PickWord(_words.GetAdjectives());

            return $"{adj}{noun}";
        }

        private string PickWord(IList<string> words)
        {
            var s = string.Empty;

            while (s == string.Empty)
            {
                s = _rng.Pick(words).Trim()
                                    .Replace("-", "")
                                    .Replace(" ", "");
            }

            return s;
        }
    }
}
