using CSharpOvning5.GarageClasses;
using CSharpOvning5.Vehicles;
using Moq;
using System.Collections.Generic;
using System.Drawing;

namespace UnitTest;

public class GarageTests
{
    private const string _validLicenseNumber = "ABC123";
    private const string _invalidLicenseNumber = "ABC12";
    private readonly Mock<Vehicle> _mockVehicle = new(_validLicenseNumber, Color.White, (uint)0);



    [Fact]
    public void Add_AddsVehiclesPastLimit()
    {
        int limit = 5;
        IGarage<Vehicle> garage = new Garage<Vehicle>(limit);
        for (int i = 0; i < limit; i++) {
            garage.Add(_mockVehicle.Object);
        }

        Assert.Throws<ArgumentOutOfRangeException>(() => garage.Add(_mockVehicle.Object));
    }

    [Theory]
    [InlineData(_validLicenseNumber)]
    [InlineData(_invalidLicenseNumber)]
    public void Remove_LicenseFormat_ThrowsArgumentException(string licenseNumber)
    {
        int limit = 5;
        IGarage<Vehicle> garage = new Garage<Vehicle>(limit);

        Assert.Throws<ArgumentException>(() => garage.Remove(licenseNumber));
    }
}
