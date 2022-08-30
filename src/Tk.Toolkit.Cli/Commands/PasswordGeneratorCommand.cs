using McMaster.Extensions.CommandLineUtils;
using Tk.Toolkit.Cli.Io;
using Tk.Toolkit.Cli.Passwords;
using Tk.Extensions.Tasks;

namespace Tk.Toolkit.Cli.Commands
{
    [Command("pw", Description = "Generate passwords")]
    internal class PasswordGeneratorCommand
    {
        private readonly IConsoleWriter _consoleWriter;
        private readonly IPasswordGenerator _pwGenerator;

        public PasswordGeneratorCommand(IConsoleWriter consoleWriter, IPasswordGenerator pwGenerator)
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
            var pws = Enumerable.Range(0, Generations.ApplyDefault(x => x <= 0, 5))
                                .Select(_ => _pwGenerator.Generate(PwLength.ApplyDefault(x => x <=  0, 16)));

            _consoleWriter.WriteMany(pws);

            return true.ToReturnCode().ToTaskResult();
        }
    }
}
