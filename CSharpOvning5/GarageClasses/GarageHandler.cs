using CSharpOvning5.Vehicles;
using System.Drawing;
using System.Text;

namespace CSharpOvning5.GarageClasses;

internal class GarageHandler(int capacity) : IHandler
{
    private IGarage<Vehicle> _garage = new Garage<Vehicle>(capacity);

    public int ParkedVehicles => _garage.ParkedVehicles;
    public string[] GetLicesenseNumbers => _garage.UsedLicenseNumbers;

    public string DisplayGarageVehicles()
    {
        StringBuilder builder = new();
        builder.AppendLine($"{Environment.NewLine}Displaying info for {_garage.Count()} vehicles.{Environment.NewLine}");
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

    public string GetVehiclesByGroup() {
        var groups = _garage.GroupBy(v => v?.GetType().Name)
                                        .Select(g =>
                                        new {
                                            VehicleType = g.Key,
                                            Count = g.Count(),
                                        });

        StringBuilder builder = new();
        foreach (var group in groups) {
            if (!string.IsNullOrWhiteSpace(group.VehicleType))
                builder.AppendLine($"Vehicle Type: {group.VehicleType}, Count: {group.Count}");
        }
        return builder.ToString();
    }

    public Vehicle? GetVehicleByLicensenumber(string number) 
        => _garage.FirstOrDefault(v => v.LicenseNumber.Equals(number, StringComparison.CurrentCultureIgnoreCase));

    // Accepts an external collectíon so that Manager.SearchForVehiclesByAttributes 
    // can keep narrowing down it's copy of 
    public IEnumerable<Vehicle> GetVehiclesByType(IEnumerable<Vehicle> collection, string type)
        => collection.Where(v => v.GetType().Name.Equals(type, StringComparison.CurrentCultureIgnoreCase));

    public IEnumerable<Vehicle> GetVehiclesByColor(IEnumerable<Vehicle> collection, Color color)
        => collection.Where(v => v.Color == color);

    public IEnumerable<Vehicle> GetVehiclesByWheelCount(IEnumerable<Vehicle> collection, int count)
        => collection.Where(v => v.NumberOfWheels >= count);
}
