using System.Security.Cryptography;

namespace Tk.Toolkit.Cli.Usernames
{
    internal class UsernameGenerator : IUsernameGenerator
    {
        private readonly Func<int, int> _pickRandom;
        private readonly IWordProvider _words;

        public UsernameGenerator(IWordProvider words) : this(RandomNumberGenerator.GetInt32, words)
        {
        }

        internal UsernameGenerator(Func<int, int> pickRandom, IWordProvider words)
        {
            _pickRandom = pickRandom;
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

            while(s == string.Empty)
            {
                s = words[_pickRandom(words.Count)].Trim()
                                                   .Replace("-", "")
                                                   .Replace(" ", "");
            }

            return s;
        }
    }
}
