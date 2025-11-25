using CSharpOvning5.Vehicles;
using System.Drawing;

namespace CSharpOvning5
{
    public interface IHandler
    {
        void AddVehicle(Vehicle vehicle);
        void CountVehicleTypes();
        string DisplayGarageVehicles();
        Vehicle? GetVehicleByLicensenumber(string number);
        IEnumerable<Vehicle> GetVehiclesByColor(Color color);
        IEnumerable<Vehicle> GetVehiclesByType(string type);
        IEnumerable<Vehicle> GetVehiclesByWheelCount(int count);
        void RemoveVehicle(string licenseNumber);
        IEnumerable<Vehicle> SearchForVehicles();
        bool Seed();
    }
}