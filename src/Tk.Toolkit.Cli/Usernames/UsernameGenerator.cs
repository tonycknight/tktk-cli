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
            var words = new[] { _words.GetAdjectives(), _words.GetWords() };
            var i = _rng.Pick(words.Length);

            return $"{PickWord(words[i])}{PickWord(_words.GetNouns())}";
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
