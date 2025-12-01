using CSharpOvning5.Vehicles;

namespace CSharpOvning5.GarageClasses
{
    internal interface IGarage<T> : IEnumerable<T> where T : Vehicle
    {
        string[] UsedLicenseNumbers { get; }
        int ParkedVehicles { get; }
        void Add(T vehicle);
        void Remove(string licenseNumber);
    }
}