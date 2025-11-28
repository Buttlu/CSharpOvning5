using System.Drawing;

namespace CSharpOvning5.Vehicles;

internal class Airplane(string licenseNumber, Color color, uint numberOfWheels, FuelType fuelType, uint numberOfSeats) 
    : Vehicle(licenseNumber, color, numberOfWheels)
{
    internal FuelType FuelType { get; } = fuelType;
    internal uint NumberOfSeats { get; } = numberOfSeats;

    internal override string ToString()
    {
        return
            $"{base.ToString()}" +
            $"Fuel Type: {FuelType}{Environment.NewLine}" +
            $"Number of Seats: {NumberOfSeats}{Environment.NewLine}";
    }
}
