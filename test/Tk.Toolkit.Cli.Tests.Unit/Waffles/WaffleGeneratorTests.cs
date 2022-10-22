using System.Linq;
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

        [Property(Verbose = true, MaxTest = 1000)]
        public bool Generate_FixedRngSeeds_IsDeterministic(PositiveInt paragraphs, bool title, PositiveInt seed)
        {            
            var results = Enumerable.Range(1, 11)
                                    .Select(_ => new WaffleGenerator(new Rng(seed.Get)))                    
                                    .Select(g => g.Generate(paragraphs.Get, title))
                                    .ToList();

            var group = results.GroupBy(sb => sb.ToString()).ToList();

            return group.Count == 1;
        }
    }
}
