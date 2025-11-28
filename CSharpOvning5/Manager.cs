using CommandLineMenu;
using ConsoleUtils;
using CSharpOvning5.Vehicles;
using System.Drawing;
using System.Text;

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

        Dictionary<string, Action<string, Color, uint, FuelType>> selectType = new() {
            { "Airplane", AddAirplane }, 
            { "Boat", AddBoat }, 
            { "Bus", AddBus }, 
            { "Car", AddCar }, 
            { "Motorcycle", AddMotorcycle }
        };

        var (_, vehicle) = _menuCli.CliMenu("Select the vehicle type", [..selectType.Keys]);        
        try {
            selectType[vehicle].Invoke(licenseNumber, color, numberOfWheels, fuelType);
        } catch (KeyNotFoundException) {
            _ui.PrintErr("Unknow vehicle selected");
        }
    }

    private void AddAirplane(string licenseNumber, Color color, uint numberOfWheels, FuelType fuelType) {
        uint numberOfSeats = (uint)_ui.GetInt("Type the number of seats: ", mustBeAboveZero: true);                    
        try {
            _handler.AddVehicle(new Airplane(licenseNumber, color, numberOfWheels, fuelType, numberOfSeats));
        } catch (ArgumentOutOfRangeException ex) {
            _ui.PrintErr(ex.Message);
        }
    }
    private void AddBoat(string licenseNumber, Color color, uint numberOfWheels, FuelType fuelType)
    {
        uint length = (uint)_ui.GetInt("Type the length: ", mustBeAboveZero: true);
        try {
            _handler.AddVehicle(new Boat(licenseNumber, color, numberOfWheels, fuelType, length));
        } catch (ArgumentOutOfRangeException ex) {
            _ui.PrintErr(ex.Message);
        }
    }
    private void AddBus(string licenseNumber, Color color, uint numberOfWheels, FuelType fuelType) {        
        bool canBend = _ui.GetBool("Can the bus bend (y/n)?: ");
        uint numberOfSeats = (uint)_ui.GetInt("Type the number of seats: ", mustBeAboveZero: true);
        try {
            _handler.AddVehicle(new Bus(licenseNumber, color, numberOfWheels, fuelType, numberOfSeats, canBend));
        } catch (ArgumentOutOfRangeException ex) {
            _ui.PrintErr(ex.Message);
        }        
    }
    private void AddCar(string licenseNumber, Color color, uint numberOfWheels, FuelType fuelType)
    {       
        uint numberOfSeats = (uint)_ui.GetInt("Type the number of seats: ", mustBeAboveZero: true);
        string manufacturer = _ui.GetString("Type the manufacturer: ");
        try {
            _handler.AddVehicle(new Car(licenseNumber, color, numberOfWheels, fuelType, numberOfSeats, manufacturer));
        } catch (ArgumentOutOfRangeException ex) {
            _ui.PrintErr(ex.Message);
        }
    }
    private void AddMotorcycle(string licenseNumber, Color color, uint numberOfWheels, FuelType fuelType)
    {       
        bool hasSideCart = _ui.GetBool("Does the motorcycle have a side-cart (y/n)?: ");
        try {
            _handler.AddVehicle(new Motorcycle(licenseNumber, color, numberOfWheels, fuelType, hasSideCart));
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
        // Gets a list of vehicles that fit certain attributes

        // Creates a copy of the parked vehicles and continuously
        // narrows it down in the below if's.
        IEnumerable<Vehicle> vehicles = _handler.GetVehicles();

        StringBuilder builder = new();
        builder.AppendLine($"{Environment.NewLine}Filtering for vehicleds that:");

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

        builder.AppendLine($"{Environment.NewLine}Found {vehicles.Count()} vehicles:");
        foreach (Vehicle vehicle in vehicles) {
            builder.AppendLine(vehicle.ToString());
        }

        _ui.Println(builder.ToString());
    }
}
