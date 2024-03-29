﻿using System.Threading.Tasks;
using FluentAssertions;
using McMaster.Extensions.CommandLineUtils;
using NSubstitute;
using Spectre.Console;
using Tk.Nuget;
using Tk.Toolkit.Cli.Commands;
using Xunit;

namespace Tk.Toolkit.Cli.Tests.Unit.Commands
{
    public class AboutCommandTests
    {
        [Fact]
        public async Task OnExecuteAsync_DefaultArgumnets_ReturnsOk()
        {
            var console = Substitute.For<IAnsiConsole>();
            var nuget = Substitute.For<INugetClient>();
            var cmd = new AboutCommand(console, nuget);
            var app = Substitute.For<CommandLineApplication>();

            var rc = await cmd.OnExecuteAsync(app);

            rc.Should().Be(0);
        }
    }
}
