using McMaster.Extensions.CommandLineUtils;
using tkdevcli.Io;
using tkdevcli.Passwords;

namespace tkdevcli.Commands
{
    [Command("pw", Description = "Generate passwords")]
    internal class PasswordGeneratorCommand
    {
        private readonly IConsoleWriter _consoleWriter;
        private readonly IPasswordGenerator _pwGenerator;

        public PasswordGeneratorCommand(IConsoleWriter consoleWriter, Passwords.IPasswordGenerator pwGenerator)
        {
            _consoleWriter = consoleWriter;
            _pwGenerator = pwGenerator;
        }

        [Option(CommandOptionType.SingleValue, Description = "The number of passwords to generate.", LongName = "gen", ShortName = "g")]
        public int Generations { get; set; }

        [Option(CommandOptionType.SingleValue, Description = "The character length of each generated password.", LongName = "len", ShortName = "l")]
        public int PwLength { get; set; }

        public Task<int> OnExecuteAsync()
        {
            Generations = Generations <= 0 ? 1 : Generations;
            PwLength = PwLength <= 1 ? 8 : PwLength;

            var pws = Enumerable.Range(0, Generations)
                .Select(_ => _pwGenerator.Generate(PwLength));

            _consoleWriter.WriteMany(pws);

            return Task.FromResult(true.ToReturnCode());
        }
    }
}
