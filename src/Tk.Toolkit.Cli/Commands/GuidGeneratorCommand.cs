using McMaster.Extensions.CommandLineUtils;
using Tk.Extensions.Tasks;
using Spectre.Console;

namespace Tk.Toolkit.Cli.Commands
{
    [Command("guid", Description = "Generate guids")]
    internal class GuidGeneratorCommand 
    {
        private readonly IAnsiConsole _console;
        internal const int DefaultGenerationCount = 5;

        public GuidGeneratorCommand(IAnsiConsole console)
        {
            _console = console;
        }

        [Option(CommandOptionType.SingleValue, Description = "The number of guids to generate.", LongName = "gen", ShortName = "g")]
        public int Generations { get; set; } = DefaultGenerationCount;

        public int OnExecute()
        {            
            var generations = Generations.ApplyDefault(x2 => x2 < 1, DefaultGenerationCount);

            var guids = Enumerable.Range(0, generations)
                                  .Select(_ => Guid.NewGuid().ToString());

            var table = guids.ToSpectreList();

            _console.Write(table);
            
            return true.ToReturnCode();
        }
    }
}