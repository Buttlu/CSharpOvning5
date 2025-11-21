using CommandLineMenu;

namespace CSharpOvning5;

internal class Manager(IUI ui, IMenuCLI menuCli)
{
    private readonly IUI _ui = ui;
    private readonly IMenuCLI _menuCli = menuCli;
    private IHandler _handler = null!;
    public void Run()
    {
        Init();
        // TODO Remove ui from garage related classes
        _handler.Seed();
        _handler.DisplayGarageVehicles();
    }

    private void Init()
    {
        _handler = new GarageHandler(10, _ui);
    }
}
