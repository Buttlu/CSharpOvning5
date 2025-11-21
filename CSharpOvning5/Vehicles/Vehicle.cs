using System.Drawing;
using System.Text.RegularExpressions;

namespace CSharpOvning5.Vehicles;

internal abstract class Vehicle(string licenseNumber, Color color, uint numberOfWheels)
{
    private string _licenseNumber = "";
    public string LicenseNumber { 
        get => _licenseNumber;
        init {
            if (!ValidateLicenseNumber().IsMatch(licenseNumber))
                throw new ArgumentException("Invalid license number format", nameof(licenseNumber));

            _licenseNumber = licenseNumber;
        }
    } 
    public Color Color { get; } = color;
    public uint NumberOfWheels { get; } = numberOfWheels;

    // No extern implementation exists,
    // just uses "extern" to avoid marking Vehicle as partial
    [GeneratedRegex("[A-Z]{3} [0-9]{2}[A-Z0-9]", RegexOptions.IgnoreCase)]
    protected static extern Regex ValidateLicenseNumber();
}
