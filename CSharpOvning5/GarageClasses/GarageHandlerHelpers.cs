using CommandLineMenu;
using ConsoleUtils;
using CSharpOvning5.Vehicles;
using System.Drawing;

namespace CSharpOvning5.GarageClasses;

internal static class GarageHandlerHelpers
{
    internal static string GetLicenseNumber(IUI ui)
    {
        do {
            string licenseNumber = ui.GetString("Type license number: ");
            if (Vehicle.ValidateLicenseNumber().IsMatch(licenseNumber)) {
                return licenseNumber;
            }
            ui.PrintErr("Invalid format, try again");
        } while (true);
    }

    internal static Color GetColor(IUI ui)
    {
        string color = ui.GetString("Type color: ");
        Color newColor = Color.FromName(color);
        // If the alpha (A) channel is 0, that means a color wasn't found 
        // (if the name is not know FromName returns new Color(0,0,0,0))
        while(newColor.A == 0) {
            ui.PrintErr("A known color was not found, please try again");
            color = ui.GetString("Color name: ");
            newColor = Color.FromName(color);
        }
        return newColor;
    }

    internal static FuelType GetFuelType(IMenuCLI ui)
    {
        // Converts all the fuel types to a string array
        var fuelTypes = Enum.GetValues<FuelType>();
        string[] fuelStrings = [.. fuelTypes.Select(x => x.ToString())];

        // Gets the fuel from a CLI menu and coverts it back to the Enum
        var (_, fuel) = ui.CliMenu("Select fuel", fuelStrings);
        FuelType fuelType = Enum.Parse<FuelType>(fuel);
        return fuelType;
    }
}
