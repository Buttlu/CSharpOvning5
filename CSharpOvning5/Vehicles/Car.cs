using System.Drawing;

namespace CSharpOvning5.Vehicles;

internal class Car(string licenseNumber, Color color, uint numberOfWheels, FuelType fuelType, uint numberOfSeats, string manufacturer) 
    : Vehicle(licenseNumber, color, numberOfWheels)
{
    public uint NumberOfSeats { get; } = numberOfSeats; 
    public FuelType FuelType { get; } = fuelType;
    public string Manufacturer { get; } = manufacturer;

    public override string ToString()
    {
        return
            $"{base.ToString()}" +
            $"Fuel Type: {FuelType}{Environment.NewLine}" +
            $"Number of Seats: {NumberOfSeats}{Environment.NewLine}" +
            $"Manufacturer: {Manufacturer}";
    }
}
