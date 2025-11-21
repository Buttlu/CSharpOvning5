using System.Drawing;

namespace CSharpOvning5.Vehicles;

internal class Car(string licenseNumber, Color color, uint numberOfWheels, FuelType fuelType, uint numberOfSeats) 
    : Vehicle(licenseNumber, color, numberOfWheels)
{
    public uint NumberOfSeats { get; } = numberOfSeats; 
    public FuelType FuelType { get; } = fuelType;
}
