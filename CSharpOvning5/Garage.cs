using CSharpOvning5.Vehicles;
using System.Collections;

namespace CSharpOvning5;

internal class Garage<T>(int capacity) : IEnumerable<T> where T : Vehicle
{
    private readonly int[] _garage = new int[capacity];

    public IEnumerator<T> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
