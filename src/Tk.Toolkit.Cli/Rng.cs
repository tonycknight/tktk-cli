using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private Random _r = new();
        public int Pick(int upper) => _r.Next(0, upper);

        public string Pick(IList<string> values) => values[Pick(values.Count)];

        public DateTime PickDate() => DateTime.Now.AddYears(-Pick(31));
    }
}
