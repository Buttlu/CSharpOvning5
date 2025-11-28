using CommandLineMenu;
using ConsoleUtils;
using CSharpOvning5.GarageClasses;
using CSharpOvning5.Vehicles;
using System.Drawing;

namespace CSharpOvning5;

internal class ManagerHelpers(IUI ui, IMenuCLI menuCli, IHandler handler)
{
    private readonly IUI _ui = ui;
    private readonly IMenuCLI _menuCli = menuCli;
    private readonly IHandler _handler = handler;

    // Gets the extra necessary attributes and creates the proper vehicles
    internal void AddAirplane(string licenseNumber, Color color, uint numberOfWheels)
    {
        FuelType fuelType = GarageHandlerHelpers.GetFuelType(_menuCli);
        uint numberOfSeats = (uint)_ui.GetInt("Type the number of seats: ", mustBeAboveZero: true);
        try {
            _handler.AddVehicle(new Airplane(licenseNumber, color, numberOfWheels, fuelType, numberOfSeats));
            _ui.Println($"Airplane {licenseNumber} parked in the garage");
        } catch (ArgumentOutOfRangeException ex) {
            _ui.PrintErr(ex.Message);
        }
    }

    internal void AddBoat(string licenseNumber, Color color, uint numberOfWheels)
    {
        FuelType fuelType = GarageHandlerHelpers.GetFuelType(_menuCli);
        uint length = (uint)_ui.GetInt("Type the length (whole meters): ", mustBeAboveZero: true);
        try {
            _handler.AddVehicle(new Boat(licenseNumber, color, numberOfWheels, fuelType, length));
            _ui.Println($"Boat {licenseNumber} parked in the garage");
        } catch (ArgumentOutOfRangeException ex) {
            _ui.PrintErr(ex.Message);
        }
    }

    internal void AddBus(string licenseNumber, Color color, uint numberOfWheels)
    {
        FuelType fuelType = GarageHandlerHelpers.GetFuelType(_menuCli);
        bool canBend = _ui.GetBool("Can the bus bend (y/n)?: ");
        uint numberOfSeats = (uint)_ui.GetInt("Type the number of seats: ", mustBeAboveZero: true);
        try {
            _handler.AddVehicle(new Bus(licenseNumber, color, numberOfWheels, fuelType, numberOfSeats, canBend));
            _ui.Println($"Bus {licenseNumber} parked in the garage");
        } catch (ArgumentOutOfRangeException ex) {
            _ui.PrintErr(ex.Message);
        }
    }

    internal void AddCar(string licenseNumber, Color color, uint numberOfWheels)
    {
        FuelType fuelType = GarageHandlerHelpers.GetFuelType(_menuCli);
        uint numberOfSeats = (uint)_ui.GetInt("Type the number of seats: ", mustBeAboveZero: true);
        string manufacturer = _ui.GetString("Type the manufacturer: ");
        try {
            _handler.AddVehicle(new Car(licenseNumber, color, numberOfWheels, fuelType, numberOfSeats, manufacturer));
            _ui.Println($"Car {licenseNumber} parked in the garage");
        } catch (ArgumentOutOfRangeException ex) {
            _ui.PrintErr(ex.Message);
        }
    }

    internal void AddMotorcycle(string licenseNumber, Color color, uint numberOfWheels)
    {
        FuelType fuelType = GarageHandlerHelpers.GetFuelType(_menuCli);
        bool hasSideCart = _ui.GetBool("Does the motorcycle have a side-cart (y/n)?: ");
        try {
            _handler.AddVehicle(new Motorcycle(licenseNumber, color, numberOfWheels, fuelType, hasSideCart));
            _ui.Println($"Motorcycle {licenseNumber} parked in the garage");
        } catch (ArgumentOutOfRangeException ex) {
            _ui.PrintErr(ex.Message);
        }
    }
}
