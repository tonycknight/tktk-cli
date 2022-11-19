using System.Security.Cryptography;
using System.Text;

namespace Tk.Toolkit.Cli.Usernames
{
    internal class UsernameGenerator : IUsernameGenerator
    {
        private readonly string[] _names = GenerateNameList();
        private readonly Func<int, int> _pickRandom;

        public UsernameGenerator() : this(RandomNumberGenerator.GetInt32)
        {
        }

        internal UsernameGenerator(Func<int, int> pickRandom)
        {
            _pickRandom = pickRandom;
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
                sb.Append(_names[index]);
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

        private static string[] GenerateNameList()
        {
            var names = new[] { 
                "test", "name", "joe",
                "Abraham","Reginald","Cheryl","Michel","Innes","Ann","Marjorie","Matthew","Mark", "Luke", "John",
                "Burt","Lionel","Humphrey","Andrew", "Jenny","Sheryl","Livia","Charlene","Winston","Heather","Michael","Sylvia","Albert",
                "Anne","Meander","Dean","Dirk","Desmond","Akiko","Jolyon","Pierre","Saoirse","Éibhear","Muircheartach","Euripides","Debonaire",
                "Gethsemane","Hermione"
            };

            return names;
        }
    }
}
