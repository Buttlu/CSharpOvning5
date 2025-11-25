using CommandLineMenu;
using CSharpOvning5.Vehicles;
using System.Drawing;

namespace CSharpOvning5;

internal class Manager(IUI ui, IMenuCLI menuCli, IHandler handler)
{
    private readonly IUI _ui = ui;
    private readonly IMenuCLI _menuCli = menuCli;
    private readonly IHandler _handler = handler;
    public void Run()
    {
        if (!_handler.Seed())
            _ui.Println("Garage is full");
        
        try {
            _handler.AddVehicle(new Motorcycle("NEJ666", Color.Red, 3, FuelType.JetFuel, true));
        } catch (ArgumentOutOfRangeException) {
            _ui.Println("Garage is full");
    }

        string vehicles = _handler.DisplayGarageVehicles();
        _ui.Println(vehicles);
        foreach (var item in _handler.SearchForVehicles()) {
            _ui.Println($"1 {item}");
        }
    }

    private void SearchForVehiclesByAttributes()
    {
        _handler = new GarageHandler(10, _ui);
    }
}
