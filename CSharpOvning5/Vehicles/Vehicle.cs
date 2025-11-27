using System.Drawing;
using System.Text.RegularExpressions;

namespace CSharpOvning5.Vehicles;

public abstract partial class Vehicle
{
    private string _licenseNumber = null!;
    public string LicenseNumber
    {
        get => _licenseNumber;
        init {
            // Validtes license numbers to follow the swedish format for car plates
            if (!ValidateLicenseNumber().IsMatch(value))
                throw new ArgumentException("Invalid license number format", value);

            _licenseNumber = value;
        }
    }
    public Color Color { get; }
    public uint NumberOfWheels { get; }

    // Regular constructor since otherwise the LicenseNumber.Init was never called
    public Vehicle(string licenseNumber, Color color, uint numberOfWheels)
    {
        LicenseNumber = licenseNumber;
        Color = color;
        NumberOfWheels = numberOfWheels;
    }

    // Validates swedish plate format, e.g.: ABC123, ABC12E
    [GeneratedRegex("^[A-Z]{3}[0-9]{2}[A-Z0-9]$", RegexOptions.IgnoreCase)]
    public static partial Regex ValidateLicenseNumber();

    public override string ToString()
    {
        return
            $"Vehicle type: {GetType().Name}{Environment.NewLine}" +
            $"License Number: {LicenseNumber}{Environment.NewLine}" +
            $"Color: {Color.Name}{Environment.NewLine}" +
            $"Number of Wheels: {NumberOfWheels}{Environment.NewLine}";
    }
}
