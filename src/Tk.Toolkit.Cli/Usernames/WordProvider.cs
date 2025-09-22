﻿namespace Tk.Toolkit.Cli.Usernames
{
    internal class WordProvider : IWordProvider
    {
        private readonly string[] _nouns;
        private readonly string[] _adjectives;
        private readonly string[] _words;

        public WordProvider()
        {
            _nouns = Resource.nouns.Split("\n");
            _adjectives = Resource.adjectives.Split("\n");
            _words = Resource.words.Split("\n");
        }

        public IList<string> GetAdjectives() => _adjectives;

        public IList<string> GetNouns() => _nouns;

        public IList<string> GetWords() => _words;
    }
}
