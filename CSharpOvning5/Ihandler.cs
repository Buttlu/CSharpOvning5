using CSharpOvning5.Vehicles;
using Microsoft.VisualBasic;
using System.Collections;
using System.Drawing;

namespace CSharpOvning5
{
    public interface IHandler
    {
        void AddVehicle(Vehicle vehicle);
        IEnumerable<Vehicle> GetVehicles();
        string DisplayGarageVehicles();
        string GetVehiclesByGroup();
        Vehicle? GetVehicleByLicensenumber(IEnumerable<Vehicle> collection, string number);
        IEnumerable<Vehicle> GetVehiclesByColor(IEnumerable<Vehicle> collection, Color color);
        IEnumerable<Vehicle> GetVehiclesByType(IEnumerable<Vehicle> collection, string type);
        IEnumerable<Vehicle> GetVehiclesByWheelCount(IEnumerable<Vehicle> collection, int count);
        void RemoveVehicle(string licenseNumber);
        bool Seed();
    }
}