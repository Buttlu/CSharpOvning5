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

        // Check that the vehicle count was increased by 1
        Assert.Equal(prevCount+1, afterCount);
    }

    [Fact]
    public void Add_AddsVehiclesPastLimit()
    {
        // Since it just needs to check for overflow, the limit can be lower.
        int smallerLimit = 1;
        IGarage<Vehicle> garage = new Garage<Vehicle>(smallerLimit);
        for (int i = 0; i < smallerLimit; i++) {
            garage.Add(_mockVehicle.Object);
        }

        Assert.Throws<ArgumentOutOfRangeException>(() => garage.Add(_mockVehicle.Object));
    }

    [Fact]
    public void Remove_RemoveFoundVehicle_LengthDecreases()
    {
        IGarage<Vehicle> garage = new Garage<Vehicle>(_limit);
        // Needs to add a vehicle in order to be able to remove it
        // If Adding fails, the test can't contine so just immediately exit
        int prevCount = CountVehicles(garage);
        garage.Add(_mockVehicle.Object);
        int afterCount = CountVehicles(garage);
        if ((prevCount + 1) != afterCount)
            Assert.Fail("Could not add vehicle to the garage. Cannot continue");

        prevCount = afterCount;
        garage.Remove(_mockVehicle.Object.LicenseNumber);
        afterCount = CountVehicles(garage);
        // Check that the vehicle count was decreased by 1
        Assert.Equal(prevCount-1, afterCount);
    }

    [Theory]
    [InlineData(_validLicenseNumber)] // Vehicle not found
    [InlineData(_invalidLicenseNumber)]
    public void Remove_LicenseFormat_ThrowsArgumentException(string licenseNumber)
    {
        IGarage<Vehicle> garage = new Garage<Vehicle>(_limit);

        Assert.Throws<ArgumentException>(() => garage.Remove(licenseNumber));
    }
}
