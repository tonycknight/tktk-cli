using McMaster.Extensions.CommandLineUtils;
using tkdevcli.Io;

namespace tkdevcli.Commands
{
    [Command("guid", Description = "Generate guids")]
    internal class GuidGeneratorCommand 
    {
        private readonly IConsoleWriter _consoleWriter;

        public GuidGeneratorCommand(IConsoleWriter consoleWriter)
        {
            _consoleWriter = consoleWriter;
        }

        [Option(CommandOptionType.SingleValue, Description = "The number of guids to generate.", LongName = "gen", ShortName = "g")]
        public int Generations { get; set; }

        public Task<int> OnExecuteAsync()
        {            
            var guids = Enumerable.Range(0, Generations.ApplyDefault(x2 => x2 <= 0, 5))
                                  .Select(_ => Guid.NewGuid().ToString());
            
            _consoleWriter.WriteMany(guids);
            
            return Task.FromResult(true.ToReturnCode());
        }
    }
}