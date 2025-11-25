using CSharpOvning5.Vehicles;
using System.Collections;

namespace CSharpOvning5;

public class Garage<T>(int capacity) : IEnumerable<T> where T : Vehicle
{
    private readonly T?[] _garage = new T?[capacity];

    public void Add(T vehicle)
    {
        for (int i = 0; i < _garage.Length; i++) {
            if (_garage[i] is null) {
                _garage[i++] = vehicle;
                return;
            }
        }
        throw new ArgumentOutOfRangeException("Garage is null");
    }

    public void Remove(string licenseNumber)
    {
        for (int i = 0; i < _garage.Length;i++) {
            if (_garage[i]?.LicenseNumber == licenseNumber) {
                _garage[i] = null;
                return;
            }
        }
        throw new ArgumentException("Vehicle not found", licenseNumber);
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < _garage.Length; i++) {
            if (_garage[i] is not null)
                yield return _garage[i]!;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
