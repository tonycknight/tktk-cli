namespace Tk.Toolkit.Cli.Waffle
{
    internal class GenContext
    {
        public GenContext(IRng rng, IPhraseProvider phrases)
        {
            Rng = rng;
            Phrases = phrases;
        }

        public IRng Rng { get; }
                
        public int OrdinalSequence { get; set; }

        public string Title { get; set; } = "";

        public RenderMode Rendering { get; set; }
        public IPhraseProvider Phrases { get; }
    }
}
