using CSharpOvning5.Vehicles;
using System.Drawing;

namespace CSharpOvning5;

public static class GarageHelpers
{
    public static Vehicle? GetVehicleByLicensenumber(this IEnumerable<Vehicle> garage, string number) => garage.FirstOrDefault(v => v.LicenseNumber == number);
    public static IEnumerable<Vehicle> GetVehiclesByType(this IEnumerable<Vehicle> garage, string type)
    {
        garage = garage.Where(v => v.GetType().Name.ToLower() == type.ToLower());
        return garage;
    }
    public static IEnumerable<Vehicle> GetVehiclesByColor(this IEnumerable<Vehicle> garage, Color color)
    {
        garage = garage.Where(v => v.Color == color);
        return garage;
    }
    public static IEnumerable<Vehicle> GetVehiclesByColor(this IEnumerable<Vehicle> garage, string color, IUI ui)
    {
        Color newColor;
        do {
            // Since people can misspell things,
            // it needs some validation to confirm wether the color was intended or misspelled
            newColor = Color.FromName(color);
            if (newColor.A == 0) {
                ui.Println("A known color was not found, either try again or type 'c' to confirm is was the right color");
                color = ui.GetString("Color name: ");
                if (color == "c")
                    return GetVehiclesByColor(garage, newColor);
            }
        } while (newColor.A == 0);
        return GetVehiclesByColor(garage, newColor);
    }
}
