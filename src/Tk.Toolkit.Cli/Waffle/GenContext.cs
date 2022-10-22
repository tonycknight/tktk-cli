namespace Tk.Toolkit.Cli.Waffle
{
    internal class GenContext
    {
        public GenContext(IRng rng) => Rng = rng;

        public IRng Rng { get; }

        public int CardinalSequence { get; set; }

        public int OrdinalSequence { get; set; }

        public string Title { get; set; } = "";

        public RenderMode Rendering { get; set; }
    }
}
