using McMaster.Extensions.CommandLineUtils;
using Tk.Extensions.Tasks;
using Spectre.Console;

namespace Tk.Toolkit.Cli.Commands
{
    [Command("guid", Description = "Generate guids")]
    internal class GuidGeneratorCommand 
    {
        private readonly IAnsiConsole _console;

        public GuidGeneratorCommand(IAnsiConsole console)
        {
            _console = console;
        }

        [Option(CommandOptionType.SingleValue, Description = "The number of guids to generate.", LongName = "gen", ShortName = "g")]
        public int Generations { get; set; }

        public Task<int> OnExecuteAsync()
        {            
            var guids = Enumerable.Range(0, Generations.ApplyDefault(x2 => x2 <= 0, 5))
                                  .Select(_ => Guid.NewGuid().ToString());

            var table = guids.ToSpectreList();

            _console.Write(table);
            
            return true.ToReturnCode().ToTaskResult();
        }
    }
}