using ConsoleUtils;

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
}
