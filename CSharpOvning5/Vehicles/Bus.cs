using System.Drawing;

namespace CSharpOvning5.Vehicles;

internal class Bus(string licenseNumber, Color color, uint numberOfWheels, FuelType fuelType, uint numberOfSeats, bool canBend) 
    : Vehicle(licenseNumber, color, numberOfWheels)
{
    public uint NumberOfSeats { get; } = numberOfSeats;
    public FuelType FuelType { get; } = fuelType;
    public bool CanBend { get; } = canBend;
}
