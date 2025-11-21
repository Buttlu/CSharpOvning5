using System.Drawing;

namespace CSharpOvning5.Vehicles;

internal class Airplane(string licenseNumber, Color color, uint numberOfWheels, FuelType fuelType, uint numberOfSeats) 
    : Vehicle(licenseNumber, color, numberOfWheels)
{
    public FuelType FuelType { get; } = fuelType;
    public uint NumberOfSeats { get; } = numberOfSeats;
}
