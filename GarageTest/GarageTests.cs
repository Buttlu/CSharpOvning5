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
    private const int _limit = 5;
    private readonly Mock<Vehicle> _mockVehicle = new(_validLicenseNumber, Color.White, (uint)0);

    // Since the length is always the same, 
    // the amount of vehicles have to be manually counted
    private static int CountVehicles(IGarage<Vehicle> garage)
    {
        int count = 0;
        foreach (var vehicle in garage) {
            if (vehicle is not null)
                count++;
        }
        return count;
    }

    [Fact]
    public void Add_AddVehicle_LengthIncreased()
    {
        // Checks if something happened by counting the number of vehicles in the garage
        IGarage<Vehicle> garage = new Garage<Vehicle>(_limit);
        int prevCount = CountVehicles(garage);

        garage.Add(_mockVehicle.Object);

        int afterCount = CountVehicles(garage);

        Assert.Equal(prevCount+1, afterCount);
    }

    [Fact]
    public void Add_AddsVehiclesPastLimit()
    {
        // Since it just needs to check for overflow, the limit can be lower.
        // Changing limit from 10 to 1 saved about 20ms. (~50 -> ~30ms)
        int smallerLimit = 1;
        IGarage<Vehicle> garage = new Garage<Vehicle>(smallerLimit);
        for (int i = 0; i < smallerLimit; i++) {
            garage.Add(_mockVehicle.Object);
        }

        Assert.Throws<ArgumentOutOfRangeException>(() => garage.Add(_mockVehicle.Object));
    }

    [Theory]
    [InlineData(_validLicenseNumber)]
    [InlineData(_invalidLicenseNumber)]
    public void Remove_LicenseFormat_ThrowsArgumentException(string licenseNumber)
    {
        IGarage<Vehicle> garage = new Garage<Vehicle>(_limit);

        Assert.Throws<ArgumentException>(() => garage.Remove(licenseNumber));
    }
}
