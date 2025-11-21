using CSharpOvning5.Vehicles;
using System.Collections;

namespace CSharpOvning5;

public class Garage<T>(int capacity) : IEnumerable<T> where T : Vehicle
{
    private readonly T[] _garage = new T[capacity];
    private int _index = 0;

    public bool Add(T vehicle)
    {
        if (_index >= capacity)
            return false;

        _garage[_index++] = vehicle;
        return true;
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < _index; i++) {
            yield return _garage[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
