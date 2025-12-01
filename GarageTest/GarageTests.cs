using CSharpOvning5.GarageClasses;
using CSharpOvning5.Vehicles;
using Moq;
using System.Drawing;

namespace UnitTest;

public class GarageTests
{

    private const int _limit = 5;
    private static readonly IHandler _handler = new GarageHandler(10);

    // Generates a mock vehicle object
    private Vehicle GetMockVehicle()
        => new Mock<Vehicle>(
            GarageHandlerHelpers.GenerateRandomLicenseNumber(_handler),
            Color.White,
            (uint)0
        ).Object;

    [Fact]
    public void Add_AddVehicle()
    {
        var garage = new Garage<Vehicle>(_limit);

        Vehicle vehicle = GetMockVehicle();

        garage.Add(vehicle);

        Assert.Contains(vehicle, garage);
    }

    [Fact]
    public void Add_AddsVehiclesPastLimit()
    {
        // Since it just needs to check for overflow, the limit can be lower.
        int smallerLimit = 1;
        var garage = new Garage<Vehicle>(smallerLimit);
        for (int i = 0; i < smallerLimit; i++) {
            garage.Add(GetMockVehicle());
        }

        Assert.Throws<ArgumentOutOfRangeException>(() => garage.Add(GetMockVehicle()));
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
        Vehicle vehicle = GetMockVehicle();
        var garage = new Garage<Vehicle>(_limit) {
            vehicle
        };

        garage.Remove(vehicle.LicenseNumber);
        Assert.DoesNotContain(vehicle, garage);
    }

    [Theory]
    [InlineData("ABC123")] // Valid but not found
    [InlineData("ABC12")]
    public void Remove_LicenseFormat_ThrowsArgumentException(string licenseNumber)
    {
        var garage = new Garage<Vehicle>(_limit);

        Assert.Throws<ArgumentException>(() => garage.Remove(licenseNumber));
    }
}
