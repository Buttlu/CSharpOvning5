using CSharpOvning5.GarageClasses;
using CSharpOvning5.Vehicles;
using Moq;
using System.Drawing;

namespace UnitTest;

public class GarageTests
{
    private const string _validLicenseNumber = "ABC123";
    private const string _invalidLicenseNumber = "ABC12";
    private const int _limit = 5;    
    private readonly Mock<Vehicle> _mockVehicle = new(_validLicenseNumber, Color.White, (uint)0);
    
    [Fact]
    public void Add_AddVehicle()
    {
        // Need to specifially test the Add method, simplified initialization bypasses Add.
#pragma warning disable IDE0028 // Simplify collection initialization
        var garage = new Garage<Vehicle>(_limit);
#pragma warning restore IDE0028 // Simplify collection initialization

        garage.Add(_mockVehicle.Object);

        Assert.Contains(_mockVehicle.Object, garage);
    }

    [Fact]
    public void Add_AddsVehiclesPastLimit()
    {
        // Since it just needs to check for overflow, the limit can be lower.
        int smallerLimit = 1;
        var garage = new Garage<Vehicle>(smallerLimit);
        for (int i = 0; i < smallerLimit; i++) {
            garage.Add(_mockVehicle.Object);
        }

        Assert.Throws<ArgumentOutOfRangeException>(() => garage.Add(_mockVehicle.Object));
    }

    [Fact]
    public void Add_AddNull_ThrowNullException() 
    {
        var garage = new Garage<Vehicle>(2);

        Assert.Throws<ArgumentNullException>(() => garage.Add(null!));
    }

    [Fact]
    public void Remove_RemoveFoundVehicle()
    {
        // Needs to add a vehicle in order to be able to remove it
        // If Adding fails, the test can't contine so just immediately exit
        var garage = new Garage<Vehicle>(_limit) {
            _mockVehicle.Object
        };

        garage.Remove(_mockVehicle.Object.LicenseNumber);
        Assert.DoesNotContain(_mockVehicle.Object, garage);
    }

    [Theory]
    [InlineData(_validLicenseNumber)] // Vehicle not found
    [InlineData(_invalidLicenseNumber)]
    public void Remove_LicenseFormat_ThrowsArgumentException(string licenseNumber)
    {
        var garage = new Garage<Vehicle>(_limit);

        Assert.Throws<ArgumentException>(() => garage.Remove(licenseNumber));
    }
}
