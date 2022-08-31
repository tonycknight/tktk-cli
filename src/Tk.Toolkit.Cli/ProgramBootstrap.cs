using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Tk.Toolkit.Cli
{
    internal static class ProgramBootstrap
    {
        public static IServiceProvider CreateServiceCollection() =>
           new ServiceCollection()
                .AddSingleton<Io.IConsoleWriter, Io.ConsoleWriter>()
                .AddSingleton<Passwords.IPasswordGenerator, Passwords.CryptoPasswordGenerator>()
                .AddSingleton<Jwts.IJwtParser, Jwts.JwtParser>()
                .BuildServiceProvider();

        public static Assembly GetAssembly() => Assembly.GetExecutingAssembly();

        public static string? GetAppVersion(this Assembly assembly)
            => assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
    }
}
