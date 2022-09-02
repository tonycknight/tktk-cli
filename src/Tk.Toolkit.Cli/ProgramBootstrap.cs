using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;

namespace Tk.Toolkit.Cli
{
    internal static class ProgramBootstrap
    {
        public static IServiceProvider CreateServiceCollection() =>
           new ServiceCollection()
                .AddSingleton<Passwords.IPasswordGenerator, Passwords.CryptoPasswordGenerator>()
                .AddSingleton<Jwts.IJwtParser, Jwts.JwtParser>()
                .AddSingleton<IAnsiConsole>(sp => AnsiConsole.Create(new AnsiConsoleSettings()))
                .BuildServiceProvider();

        public static Assembly GetAssembly() => Assembly.GetExecutingAssembly();

        public static string? GetAppVersion(this Assembly assembly)
            => assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
    }
}
