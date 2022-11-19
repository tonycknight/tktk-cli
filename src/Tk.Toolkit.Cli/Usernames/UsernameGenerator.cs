using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;
using Tk.Toolkit.Cli.Waffle;

namespace Tk.Toolkit.Cli.Usernames
{
    internal class UsernameGenerator : IUsernameGenerator
    {
        private readonly string[] _names;
        private readonly Func<int, int> _pickRandom;
        private readonly IPhraseProvider _phrases;

        public UsernameGenerator(IPhraseProvider phrases) : this(RandomNumberGenerator.GetInt32, phrases)
        {
        }

        internal UsernameGenerator(Func<int, int> pickRandom, IPhraseProvider phrases)
        {
            _pickRandom = pickRandom;
            _phrases = phrases;
            _names = _phrases.GetPhrases(PhraseKind.FirstName);
        }

        public string Generate(int minLength) => GenerateRandomUsername(minLength);

        private string GenerateRandomUsername(int minLength)
        {
            var sb = new StringBuilder();
            
            var indexes = GetNameIndexes().GetUniques();

            foreach ( var index in indexes ) 
            {
                if(sb.Length >= minLength)
                {
                    return sb.ToString();
                }

                sb.Append(_names[index].ToLower());
            }
            
            return sb.ToString();
        }

        private IEnumerable<int> GetNameIndexes()
        {
            while (true)
            {
                yield return _pickRandom(_names.Length);
            }
        }
    }
}
