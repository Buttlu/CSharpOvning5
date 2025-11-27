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

        // Dictionary that holds the main menu options and which method they use
        Dictionary<string, Action> mainMenuOptions = new() {
            { "Add vehicle", AddVehicle },
            { "Remove vehicle", RemoveVehicle },
            { "Display all vehicles", DisplayGarage },
            { "Display groups", DisplayVehicleGroups },
            { "Find vehicle by license number", SearchForVehicleByLicenseNumber },
            { "Find vehicles by attributes", SearchForVehiclesByAttributes },
            { "Quit", () => Environment.Exit(0) }
        };
        int index;
        string key;
        bool first = true;
        do {
            (index, key) = _menuCli.CliMenu("Main menu",  first, [.. mainMenuOptions.Keys]);
            mainMenuOptions[key].Invoke();
            first = false;
        } while (true);
    }

    private void AddVehicle()
    {
        try {
            _handler.AddVehicle(new Motorcycle("NEJ666", Color.Red, 3, FuelType.JetFuel, true));
        } catch (ArgumentOutOfRangeException ex) {
            _ui.PrintErr(ex.Message);
        }
    }

    private void RemoveVehicle()
    {
        string licenseNumber = GarageHandlerHelpers.GetLicenseNumber(_ui);
        try {
            _handler.RemoveVehicle(licenseNumber);
        } catch (ArgumentException ex) {
            _ui.PrintErr(ex.Message);
        }
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

    private void SearchForVehicleByLicenseNumber()
    {
        string licenseNumber = GarageHandlerHelpers.GetLicenseNumber(_ui);
        Vehicle? vehicle = _handler.GetVehicleByLicensenumber(licenseNumber);
        if (vehicle is null) {
            _ui.PrintErr($"No vehicle found with license number \"{licenseNumber}\"");
            return;
        }
        _ui.Println(vehicle.ToString() + Environment.NewLine);
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

        _ui.Println("Type the color: ");
        Color color = GarageHandlerHelpers.GetColor(_ui);
    }
}
