using System.Drawing;

namespace CSharpOvning5.Vehicles;

internal class Boat(string licenseNumber, Color color, uint numberOfWheels, FuelType fuelType, uint length) 
    : Vehicle(licenseNumber, color, numberOfWheels)
{
    public FuelType FuelType { get; } = fuelType;
    public uint Length { get; } = length;

    public override string ToString()
    {
        return
            $"{base.ToString()}" +
            $"Fuel Type: {FuelType}{Environment.NewLine}" +
            $"Length: {Length}{Environment.NewLine} meters";
    }
}
