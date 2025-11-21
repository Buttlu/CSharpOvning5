using CSharpOvning5.Vehicles;
using System.Drawing;

namespace CSharpOvning5;

public class GarageHandler(int capacity, IUI ui) : IHandler
{
    private IUI _ui = ui;
    private Garage<Vehicle> _garage = new(capacity);

    public void DisplayGarageVehicles()
    {

    }

    public void CountVehicleTypes()
    {

    }

    public bool AddVehicle(Vehicle vehicle)
    {
        return true;
    }

    public bool RemoveVehicle(Vehicle vehicle)
    {
        return true;
    }

    public bool SeedGarage()
    {
        return true;
    }

    public IEnumerable<Vehicle> SearchForVehicles()
    {
        return null;
    }
}
