using CommandLineMenu;
using ConsoleUtils;

namespace CSharpOvning5;

internal class Program
{
    static void Main(string[] args)
    {
        IUI ui = new ConsoleUI();
        IMenuCLI menuCli = new MenuUI(ui);
        IHandler handler = new GarageHandler(10);
        Manager manager = new(ui, menuCli, handler);
        manager.Run();
    }
}
