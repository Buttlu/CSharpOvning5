using CommandLineMenu;
using ConsoleUtils;
using CSharpOvning5.Vehicles;
using System.Drawing;

namespace CSharpOvning5;

internal class Manager(IUI ui, IMenuCLI menuCli)
{
    private readonly IUI _ui = ui;
    private readonly IMenuCLI _menuCli = menuCli;
    private IHandler _handler = null!;
    public void Run()
    {
        CreateGarage();

        if (!_handler.Seed())
            _ui.Println("Garage is full");

        // Dictionary that holds the main menu options and which method they use
        Dictionary<string, Action> mainMenuOptions = new() {
            { "Create a new garage", CreateGarage },
            { "Park vehicle", AddVehicle },
            { "Remove vehicle", RemoveVehicle },
            { "Display all vehicles", DisplayGarage },
            { "Display groups", DisplayVehicleGroups },
            { "Find vehicle by license number", SearchForVehicleByLicenseNumber },
            { "Find vehicles by attributes", SearchForVehiclesByAttributes },
            { "Quit", () => Environment.Exit(0) }
        };
        
        string key;
        bool first = true;
        do {
            (_, key) = _menuCli.CliMenu("Main menu",  first, [.. mainMenuOptions.Keys]);
            mainMenuOptions[key].Invoke();
            first = false;
        } while (true);
    }

    private void CreateGarage()
    {
        if (_handler is not null) {
            _ui.Println("This will replace the current garage, type 'q' to cancel");
            string? input = _ui.GetInput();
            if (input is not null && Char.ToLower(input[0]) == 'q') return;
        }

        int capacity = _ui.GetInt("Type number of vehicles that can be parked: ", mustBeAboveZero: true);
        _handler = new GarageHandler(capacity);
    }

    private void AddVehicle()
    {
        string licenseNumber = GarageHandlerHelpers.GetLicenseNumber(_ui);        
        Color color = GarageHandlerHelpers.GetColor(_ui);
        FuelType fuelType = GarageHandlerHelpers.GetFuelType(_menuCli);
        uint numberOfWheels = (uint)_ui.GetInt("Type the number of wheels: ", mustBeAboveZero: true);
        
        try {
            _handler.AddVehicle(new Motorcycle(licenseNumber, color, numberOfWheels, fuelType, true));
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
        // "Do you want to filter for X?" (y / n)
        // if y 
        //     GarageHandlerHelpers.GetX();
        //     vehicles = _handler.GetVehicleByX();
    }
}
