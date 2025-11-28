using System.Drawing;

namespace CSharpOvning5.Vehicles
{
    internal class Motorcycle(string licenseNumber, Color color, uint numberOfWheels, FuelType fuelType, bool hasSideCart) 
        : Vehicle(licenseNumber, color, numberOfWheels)
    {
        internal FuelType FuelType { get; } = fuelType;
        internal bool HasSideCart { get; } = hasSideCart;

        public override string ToString()
        {
            return
                $"{base.ToString()}" +
                $"Fuel Type: {FuelType}{Environment.NewLine}" +
                $"Has a Side Cart: {(HasSideCart ? "Yes" : "No")}{Environment.NewLine}";
        }
    }
}
