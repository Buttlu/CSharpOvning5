using System.Drawing;

namespace CSharpOvning5.Vehicles;

internal class Bus(string licenseNumber, Color color, uint numberOfWheels, FuelType fuelType, uint numberOfSeats, bool canBend) 
    : Vehicle(licenseNumber, color, numberOfWheels)
{
    internal uint NumberOfSeats { get; } = numberOfSeats;
    internal FuelType FuelType { get; } = fuelType;
    internal bool CanBend { get; } = canBend;

    internal override string ToString()
    {
        return
            $"{base.ToString()}" +
            $"Fuel Type: {FuelType}{Environment.NewLine}" +
            $"number of Seats: {NumberOfSeats}{Environment.NewLine}" +
            $"Can it Bend: {(CanBend ? "Yes" : "No")}{Environment.NewLine}";
    }
}
