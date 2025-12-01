using CommandLineMenu;
using ConsoleUtils;
using CSharpOvning5.Vehicles;
using System.Drawing;

namespace CSharpOvning5.GarageClasses;

internal static class GarageHandlerHelpers
{
    internal static string GetLicenseNumberFromUser(IUI ui, IHandler handler)
    {
        do {
            ui.Print("Type license number (leave blank to get random): ");
            string? licenseNumber = ui.GetInput();
            
            if (string.IsNullOrWhiteSpace(licenseNumber)) {
                licenseNumber = GenerateRandomLicenseNumber(handler);
                ui.Println($"Generated: {licenseNumber}");
                return licenseNumber;
            } else if (handler.GetLicesenseNumbers.Contains(licenseNumber, StringComparer.OrdinalIgnoreCase))
                ui.PrintErr("License number is already in use");            
            else if (Vehicle.ValidateLicenseNumber().IsMatch(licenseNumber))
                return licenseNumber;            
            else
                ui.PrintErr("Invalid license number, try again");
        } while (true);
    }

    internal static Color GetColorFromUser(IUI ui)
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

    internal static FuelType GetFuelTypeFromUser(IMenuCLI ui)
    {
        // Converts all the fuel types to a string array
        var fuelTypes = Enum.GetValues<FuelType>();
        string[] fuelStrings = [.. fuelTypes.Select(x => x.ToString())];

        // Gets the fuel from a CLI menu and coverts it back to the Enum
        var (_, fuel) = ui.CliMenu("Select fuel", fuelStrings);
        FuelType fuelType = Enum.Parse<FuelType>(fuel);
        return fuelType;
    }

    internal static string GenerateRandomLicenseNumber(IHandler handler)
    {
        Random rnd = new();
        char[] chars = [
            'A','B','C','D','E','F',
            'G','H','I','J','K','L',
            'M','N','O','P','Q','R',
            'S','T','U','V','W','X',
            'Y','Z','0','1','2','3',
            '4','5','6','7','8','9'];
        int numberOfLetters = 26;
        // has to match [A-Z]{3}[0-9]{2}[A-Z0-9]
        string output = new([
            chars[rnd.Next(numberOfLetters)], // [A-Z]
            chars[rnd.Next(numberOfLetters)], // [A-Z]
            chars[rnd.Next(numberOfLetters)], // [A-Z]
            chars[rnd.Next(numberOfLetters, chars.Length)], // [0-9]
            chars[rnd.Next(numberOfLetters, chars.Length)], // [0-9]
            chars[rnd.Next(chars.Length)], // [A-Z0-9]
            ]);

        if (handler.GetLicesenseNumbers.Contains(output)) {
            output = GenerateRandomLicenseNumber(handler);
        }

        return output;
    }
}
