using CSharpOvning5.Vehicles;
using System.Collections;

namespace CSharpOvning5.GarageClasses;

internal class Garage<T>(int capacity) : IEnumerable<T>, IGarage<T> where T : Vehicle
{
    private readonly T?[] _garage = new T?[capacity];

    public void Add(T vehicle)
    {
        ArgumentNullException.ThrowIfNull(vehicle, nameof(vehicle));

        // Loops through the array until it finds the first null entry, and replaces it
        for (int i = 0; i < _garage.Length; i++) {
            if (_garage[i] is null) {
                _garage[i++] = vehicle;
                return;
            }
        }
        // Throws and exception if the array is full
        throw new ArgumentOutOfRangeException(nameof(vehicle), "Garage is null");
    }

    public void Remove(string licenseNumber)
    {
        // Validates the license number first. 
        if (!Vehicle.ValidateLicenseNumber().IsMatch(licenseNumber))
            throw new ArgumentException("Invalid license number", nameof(licenseNumber));

        licenseNumber = licenseNumber.ToLower();

        // Tries to find a matching license number and sets it to null if found
        for (int i = 0; i < _garage.Length; i++) {
            if (_garage[i]?.LicenseNumber.ToLower() == licenseNumber) {
                _garage[i] = null;
                return;
            }
        }
        throw new ArgumentException("Vehicle not found", nameof(licenseNumber));
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
