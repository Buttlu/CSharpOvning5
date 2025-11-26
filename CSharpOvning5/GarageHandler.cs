using CSharpOvning5.Vehicles;
using System.Drawing;
using System.Text;

namespace CSharpOvning5;

public class GarageHandler(int capacity) : IHandler
{
    private Garage<Vehicle> _garage = new(capacity);

    public string DisplayGarageVehicles()
    {
        StringBuilder builder = new();
        foreach (var vehicle in _garage) {
            builder.AppendLine(vehicle.ToString());
        }
        return builder.ToString();
    }

    public IEnumerable<Vehicle> GetVehicles()
    {
        return _garage.ToList();
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
        try {
            AddVehicle(new Motorcycle("ABC123", Color.Red, 2, FuelType.Gasoline, false));
            AddVehicle(new Airplane("ABC12C", Color.White, 22, FuelType.JetFuel, 200));
            AddVehicle(new Car("CAR420", Color.Yellow, 4, FuelType.Electric, 5, "Volvo"));
            AddVehicle(new Car("BAR39F", Color.Green, 4, FuelType.Gasoline, 2, "Ferrari"));
            AddVehicle(new Boat("ZOO100", Color.Blue, 0, FuelType.JetFuel, 15));
        } catch (ArgumentOutOfRangeException) {
            return false;
        }

        return true;
    }

    public Vehicle? GetVehicleByLicensenumber(IEnumerable<Vehicle> collection, string number) 
        => collection.FirstOrDefault(v => v.LicenseNumber == number);

    public IEnumerable<Vehicle> GetVehiclesByType(IEnumerable<Vehicle> collection, string type)
        => collection.Where(v => v.GetType().Name.Equals(type, StringComparison.CurrentCultureIgnoreCase));

    public IEnumerable<Vehicle> GetVehiclesByColor(IEnumerable<Vehicle> collection, Color color)
        => collection.Where(v => v.Color == color);

    public IEnumerable<Vehicle> GetVehiclesByWheelCount(IEnumerable<Vehicle> collection, int count)
        => collection.Where(v => v.NumberOfWheels >= count);
}
