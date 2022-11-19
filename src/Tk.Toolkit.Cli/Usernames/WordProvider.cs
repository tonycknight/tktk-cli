namespace Tk.Toolkit.Cli.Usernames
{
    internal class WordProvider : IWordProvider
    {
        private readonly string[] _nouns;
        private readonly string[] _adjectives;

        public WordProvider() 
        {
            _nouns = Resource.nouns.Split('\r', '\n');
            _adjectives = Resource.adjectives.Split(Environment.NewLine);
        }

        public IList<string> GetAdjectives() => _adjectives;

        public IList<string> GetNouns() => _nouns;
    }
}
