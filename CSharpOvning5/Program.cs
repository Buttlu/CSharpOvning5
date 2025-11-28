using CommandLineMenu;
using ConsoleUtils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CSharpOvning5;

internal class Program
{
    static void Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
                        .ConfigureServices(services =>
                        {
                            services.AddSingleton<IUI, ConsoleUI>();
                            services.AddSingleton<IMenuCLI, MenuUI>();
                            services.AddSingleton<Manager>();
                        })
                        .UseConsoleLifetime()
                        .Build();
        host.Services.GetRequiredService<Manager>().Run();
    }
}
