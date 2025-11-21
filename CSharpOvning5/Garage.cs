using CSharpOvning5.Vehicles;
using System.Collections;

namespace CSharpOvning5;

internal class Garage<T>(int capacity) : IEnumerable<T> where T : Vehicle
{
    private readonly T[] _garage = new T[capacity];

    public IEnumerator<T> GetEnumerator()
    {
        foreach (T vehicle in _garage) {
            yield return vehicle;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
