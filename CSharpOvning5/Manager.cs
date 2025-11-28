using CommandLineMenu;
using ConsoleUtils;
using CSharpOvning5.GarageClasses;
using CSharpOvning5.Vehicles;
using System.Drawing;
using System.Text;

namespace CSharpOvning5;

internal class Manager(IUI ui, IMenuCLI menuCli)
{
    private readonly IUI _ui = ui;
    private readonly IMenuCLI _menuCli = menuCli;
    private IHandler _handler = null!;
    internal void Run()
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
        uint numberOfWheels = (uint)_ui.GetInt("Type the number of wheels: ", mustBeAboveZero: true);

        // Uses a dictionary so that the keys can be used in the CLI
        // and it links each vehicle to the appropriate method
        ManagerHelpers helpers = new(_ui, _menuCli, _handler);
        Dictionary<string, Action<string, Color, uint>> selectType = new() {
            { "Airplane", helpers.AddAirplane }, 
            { "Boat", helpers.AddBoat }, 
            { "Bus", helpers.AddBus }, 
            { "Car", helpers.AddCar }, 
            { "Motorcycle", helpers.AddMotorcycle }
        };

        var (_, vehicle) = _menuCli.CliMenu("Select the vehicle type", [..selectType.Keys]);        
        try {
            selectType[vehicle].Invoke(licenseNumber, color, numberOfWheels);
        } catch (KeyNotFoundException) {
            _ui.PrintErr("Unknow vehicle selected");
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
        // Gets a list of vehicles that fit certain attributes

        // Creates a copy of the parked vehicles and continuously
        // narrows it down in the below if's.
        IEnumerable<Vehicle> vehicles = _handler.GetVehicles();

        StringBuilder builder = new();
        builder.AppendLine($"{Environment.NewLine}Filtering for vehicleds that:");

        // Filters applies optional filters for number of wheels, color, and vehicle type
        if (_ui.GetBool("Do you want to filter for number of wheels (y/n)? ")) {
            int numberOfWheels = _ui.GetInt("Type the number of wheels: ", mustBeAboveZero: true);
            vehicles = _handler.GetVehiclesByWheelCount(vehicles, numberOfWheels);
            builder.AppendLine($"\thave {numberOfWheels} number of wheels");
        }

        if (_ui.GetBool("Do you want to filter for the color (y/n)? ")) {
            Color color = GarageHandlerHelpers.GetColor(_ui);
            vehicles = _handler.GetVehiclesByColor(vehicles, color);
            builder.AppendLine($"\tare {color.Name}");
        }

        if (_ui.GetBool("Do you want to filter for the type of vehicle (y/n)? ")) {
            string type = _ui.GetString("Type the vehicle type: ");
            vehicles = _handler.GetVehiclesByType(vehicles, type);
            builder.AppendLine($"\tis a {type.ToLower()}");
        }

        // Compiles the number of vehicles found that matches and prints out their info
        builder.AppendLine($"{Environment.NewLine}Found {vehicles.Count()} vehicles:");
        foreach (Vehicle vehicle in vehicles) {
            builder.AppendLine(vehicle.ToString());
        }

        _ui.Println(builder.ToString());
    }
}
