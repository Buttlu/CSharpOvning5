using CommandLineMenu;
using ConsoleUtils;

namespace CSharpOvning5;

internal class Manager(IUI ui, IMenuCLI menuCli)
{
    private readonly IUI _ui = ui;
    private readonly IMenuCLI _menuCli = menuCli;

    public void Run()
    {

    }
}
