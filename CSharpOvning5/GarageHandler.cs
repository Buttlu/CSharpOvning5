using CSharpOvning5.Vehicles;
using System.Drawing;
using System.Text;

namespace CSharpOvning5;

public class GarageHandler(int capacity) : IHandler
{
    private IUI _ui = ui;
    private Garage<Vehicle> _garage = new(capacity);

    public string DisplayGarageVehicles()
    {
        StringBuilder builder = new();
        foreach (var vehicle in _garage) {
            builder.AppendLine(vehicle.ToString());
        }
        return builder.ToString();
    }

    public void CountVehicleTypes()
    {

    }

    public void AddVehicle(Vehicle vehicle)
    {
        _garage.Add(vehicle);
        }
        
    public void RemoveVehicle(string licenseNumber)
    {
        _garage.Remove(licenseNumber);
    }

    public bool Seed()
    {
        AddVehicle(new Motorcycle("ABC 123", Color.Red, 2, FuelType.Gasoline, false));
        AddVehicle(new Airplane("ABC 12C", Color.White, 22, FuelType.JetFuel, 200));
        AddVehicle(new Car("CAR 420", Color.Yellow, 4, FuelType.Electric, 5, "Volvo"));
        AddVehicle(new Car("BAR 39F", Color.Green, 4, FuelType.Gasoline, 2, "Ferrari"));
        AddVehicle(new Boat("ZOO 100", Color.Blue, 0, FuelType.JetFuel, 15));

        return true;
    }

    public IEnumerable<Vehicle> SearchForVehicles()
    {
        IEnumerable<Vehicle> foundVehicles = _garage.ToList();

        return foundVehicles.GetVehiclesByColor(Color.White).GetVehiclesByType(typeof(Motorcycle).Name);
    }
}
