using System.Drawing;

namespace CSharpOvning5.Vehicles
{
    internal class Motorcycle(string licenseNumber, Color color, uint numberOfWheels, FuelType fuelType, bool hasSideCart) 
        : Vehicle(licenseNumber, color, numberOfWheels)
    {
        public FuelType FuelType { get; } = fuelType;
        // TODO: maybe add Add...() & Remove...() SideCart functions. 
        public bool HasSideCart { get; } = hasSideCart;

        public override string ToString()
        {
            return
                $"{base.ToString()}" +
                $"Fuel Type: {FuelType}{Environment.NewLine}" +
                $"Has a Side Cart: {(HasSideCart ? "Yes" : "No")}{Environment.NewLine}";
        }
    }
}
