using System.Drawing;

namespace CSharpOvning5.Vehicles
{
    internal class Motorcycle(string licenseNumber, Color color, uint numberOfWheels, FuelType fuelType, bool hasSideCart) 
        : Vehicle(licenseNumber, color, numberOfWheels)
    {
        internal FuelType FuelType { get; } = fuelType;
        // TODO: maybe add Add...() & Remove...() SideCart functions. 
        internal bool HasSideCart { get; } = hasSideCart;

        internal override string ToString()
        {
            return
                $"{base.ToString()}" +
                $"Fuel Type: {FuelType}{Environment.NewLine}" +
                $"Has a Side Cart: {(HasSideCart ? "Yes" : "No")}{Environment.NewLine}";
        }
    }
}
