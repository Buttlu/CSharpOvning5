using ConsoleUtils;
using System.Drawing;

namespace CSharpOvning5;

internal static class GarageHandlerHelpers
{
    public static string GetLicenseNumber(IUI ui)
    {
        do {
            string licenseNumber = ui.GetString("Type license number: ");
            if (Vehicles.Vehicle.ValidateLicenseNumber().IsMatch(licenseNumber)) {
                return licenseNumber;
            }
            ui.PrintErr("Invalid format, try again");
        } while (true);
    }

    public static Color GetColor(IUI ui)
    {
        string color = ui.GetString("Type color: ");
        Color newColor;
        do {
            newColor = Color.FromName(color);
            ui.Println("A known color was not found, please try again");
            color = ui.GetString("Color name: ");
        } while (newColor.A == 0);
        return newColor;
    }
}
