using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FsCheck;
using FsCheck.Xunit;
using NSubstitute;
using Tk.Toolkit.Cli.Waffle;

namespace Tk.Toolkit.Cli.Tests.Unit.Waffles
{
    public class WaffleGeneratorTests
    {
        [Property(Verbose = true, MaxTest = 1000)]
        public bool Generate_GeneratesText(PositiveInt paragraphs, bool title)
        {
            var rng = Substitute.For<IRng>();
            
            var gen = new WaffleGenerator(rng);

            var result = gen.Generate(paragraphs.Get, title);

            return result.ToString().Trim().Length > 0;
        }
    }
}
