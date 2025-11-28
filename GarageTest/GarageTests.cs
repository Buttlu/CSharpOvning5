using Moq;
using CSharpOvning5.GarageClasses;
using CSharpOvning5.Vehicles;
using System.Drawing;

namespace UnitTest;

public class GarageTests
{
    private readonly Mock<Vehicle> _mockVehicle = new("ABC123", Color.White, (uint)0);



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
}
