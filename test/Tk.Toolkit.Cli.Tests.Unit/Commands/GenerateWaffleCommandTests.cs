using System;
using System.Collections.Generic;
using System.Linq;
using FsCheck;
using FsCheck.Xunit;
using NSubstitute;
using Spectre.Console;
using Tk.Toolkit.Cli.Commands;

namespace Tk.Toolkit.Cli.Tests.Unit.Commands
{
    public class GenerateWaffleCommandTests
    {
        [Property(Verbose = true)]
        public bool OnExecute_FixedSeed_SingleParagraph_DeterministicBehaviour(PositiveInt paragraphs, bool showTitle, PositiveInt seed)
        {
            var outputs = new List<(int, int)>();
            var console = Substitute.For<IAnsiConsole>();
            console.When(ac => ac.Write(Arg.Any<Text>())).Do(cb =>
            {
                var arg = cb[0] as Spectre.Console.Text;
                outputs.Add((arg!.Length, arg.Lines));
            });

            Func<GenerateWaffleCommand> cmd = () => new GenerateWaffleCommand(console)
            {
                IncludeTitle = showTitle,
                Paragraphs = paragraphs.Get,
                Seed = seed.Get
            };

            var runes = Enumerable.Range(1, 11)
                .Select(_ => cmd())
                .Select(c => c.OnExecute())
                .ToList();


            return outputs.GroupBy(x => x).Count() == 1;
        }
    }
}
