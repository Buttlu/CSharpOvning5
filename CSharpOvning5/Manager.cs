using CommandLineMenu;
using ConsoleUtils;
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
    }

    private void AddVehicle()
    {
        try {
            _handler.AddVehicle(new Motorcycle("NEJ666", Color.Red, 3, FuelType.JetFuel, true));
        } catch (ArgumentOutOfRangeException) {
            _ui.Println("Garage is full");
        }
    }

    private void RemoveVehicle()
    {
        // logic to get the license number from the user
        _handler.RemoveVehicle("ABC123");
    }

    private void DisplayGarage()
    {
        string vehicles = _handler.DisplayGarageVehicles();
        _ui.Println(vehicles);
    }

    private void DisplayVehicleGroups()
    {
        string vehicleGroups = _handler.GetVehiclesByGroup();
        _ui.Println(vehicleGroups);
    }

    private void SearchForVehiclesByLicenseNumber()
    {

    }

    private void SearchForVehiclesByAttributes()
    {
        IEnumerable<Vehicle> vehicles = _handler.GetVehicles();
        // ------------- WIP -------------

        _ui.Println("Searching for vehicles using filters. Leave blank to ignore");

        _ui.Println("Type number of wheels: ");
        string? input = _ui.GetInput();
        if (!string.IsNullOrWhiteSpace(input) && int.TryParse(input, out int wheelCount)) {
            vehicles = _handler.GetVehiclesByWheelCount(vehicles, wheelCount);
        }

        string color = _ui.GetString("Type color: ");
        Color newColor = Color.FromName(color);
        while (newColor.A == 0) {
            _ui.Println("A known color was not found, please try again");
            color = _ui.GetString("Color name: ");
            newColor = Color.FromName(color);
        }
    }
}
