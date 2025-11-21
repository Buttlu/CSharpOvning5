using CommandLineMenu;
using ConsoleUtils;

namespace CSharpOvning5;

internal class Program
{
    static void Main(string[] args)
    {
        IUI ui = new ConsoleUI();
        IMenuCLI menuCli = new MenuUI(ui);
        Manager handler = new(ui, menuCli);
        handler.Run();
    }
}
