using System.Drawing;

namespace CSharpOvning5.Vehicles;

internal class Car(string licenseNumber, Color color, uint numberOfWheels, FuelType fuelType, uint numberOfSeats, string manufacturer) 
    : Vehicle(licenseNumber, color, numberOfWheels)
{
    internal uint NumberOfSeats { get; } = numberOfSeats; 
    internal FuelType FuelType { get; } = fuelType;
    internal string Manufacturer { get; } = manufacturer;

    internal override string ToString()
    {
        return
            $"{base.ToString()}" +
            $"Fuel Type: {FuelType}{Environment.NewLine}" +
            $"Number of Seats: {NumberOfSeats}{Environment.NewLine}" +
            $"Manufacturer: {Manufacturer}{Environment.NewLine}";
    }
}
