namespace Tk.Toolkit.Cli
{
    internal interface IRng
    {
        int Pick(int upper);
        string Pick(IList<string> values);
        DateTime PickDate();
    }

    internal class Rng : IRng
    {
        private readonly Random _r;
        public Rng() => _r = new();

        public Rng(int seed) => _r = new Random(seed);

        public int Pick(int upper) => _r.Next(0, upper);

        public string Pick(IList<string> values) => values[Pick(values.Count)];

        public DateTime PickDate() => DateTime.Now.AddYears(-Pick(31));
    }
}
