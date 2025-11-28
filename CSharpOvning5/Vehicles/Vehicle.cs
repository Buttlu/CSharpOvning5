using System.Drawing;
using System.Text.RegularExpressions;

namespace CSharpOvning5.Vehicles;

internal abstract partial class Vehicle
{
    private string _licenseNumber = null!;
    internal string LicenseNumber
    {
        get => _licenseNumber;
        init {
            // Validtes license numbers to follow the swedish format for car plates
            if (!ValidateLicenseNumber().IsMatch(value))
                throw new ArgumentException("Invalid license number format", value);

            _licenseNumber = value;
        }
    }
    internal Color Color { get; }
    internal uint NumberOfWheels { get; }

    // Regular constructor since otherwise the LicenseNumber.Init was never called
    internal Vehicle(string licenseNumber, Color color, uint numberOfWheels)
    {
        LicenseNumber = licenseNumber;
        Color = color;
        NumberOfWheels = numberOfWheels;
    }

    // Validates swedish plate format, e.g.: ABC123, ABC12E
    [GeneratedRegex("^[A-Z]{3}[0-9]{2}[A-Z0-9]$", RegexOptions.IgnoreCase)]
    internal static partial Regex ValidateLicenseNumber();

    internal override string ToString()
    {
        return
            $"Vehicle type: {GetType().Name}{Environment.NewLine}" +
            $"License Number: {LicenseNumber}{Environment.NewLine}" +
            $"Color: {Color.Name}{Environment.NewLine}" +
            $"Number of Wheels: {NumberOfWheels}{Environment.NewLine}";
    }
}
