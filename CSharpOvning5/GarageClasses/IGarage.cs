using CSharpOvning5.Vehicles;

namespace CSharpOvning5.GarageClasses
{
    internal interface IGarage<T> : IEnumerable<T> where T : Vehicle
    {
        void Add(T vehicle);
        void Remove(string licenseNumber);
        //IEnumerator<T> GetEnumerator();
    }
}