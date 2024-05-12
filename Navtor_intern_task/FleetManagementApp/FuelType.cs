namespace FleetManagementApp;

public enum FuelType
{
    Diesel,
    HeavyFuel
}

public static class FuelConstants
{
    public const double DieselDensity = 0.85;
    public const double HeavyFuelDensity = 0.95;
}

public static class FuelTypeExtensions
{
    public static double GetDensity(this FuelType fuelType)
    {
        return fuelType switch
        {
            FuelType.Diesel => FuelConstants.DieselDensity,
            FuelType.HeavyFuel => FuelConstants.HeavyFuelDensity,
            _ => throw new ArgumentOutOfRangeException(nameof(fuelType), fuelType, null)
        };
    }
}