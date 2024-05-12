using FleetManagementApp.Exceptions;

namespace FleetManagementApp;

public class Tank
{
    public double CurrentCapacityL { get; private set; }
    public double MaxCapacityL { get; }
    public FuelType Type { get; }
    public Guid Id { get; }
    public double CurrentMassKg;

    public Tank(double maxCapacityL, FuelType type)
    {
        ValidateMaxCapacityL(maxCapacityL);
        
        MaxCapacityL = maxCapacityL;
        Type = type;
        CurrentCapacityL = 0;
        CurrentMassKg = 0;
        Id = Guid.NewGuid();
    }
    
    public void Refuel(double amountL)
    {
        if (CurrentCapacityL + amountL > MaxCapacityL) throw new TankOverfillException("The tank is full.");
        CurrentCapacityL += amountL;
        CurrentMassKg += GetMassOfFuelKg(amountL, Type);
    }

    private static void ValidateMaxCapacityL(double maxCapacityL)
    {
        if (maxCapacityL <= 0)
        {
            throw new InvalidTankCapacityException(
                "The capacity of the tank must be greater than 0");
        }
    }

    public void EmptyFully()
    {
        if(CurrentCapacityL == 0) throw new EmptyingEmptyTankException(
            "The tank is already empty.");
        
        CurrentCapacityL = 0;
        CurrentMassKg = 0;
    }

    public void EmptyPartially(double amountL)
    {
        if(CurrentCapacityL == 0) throw new EmptyingEmptyTankException(
            "The tank is already empty.");
        
        if (CurrentCapacityL < amountL)
        {
            throw new EmptyingTooMuchFuelException(
                "The tank cannot be emptied, the amount is greater than the current capacity");
        }
        
        var fuelMass = GetMassOfFuelKg(amountL, Type);
        CurrentCapacityL -= amountL;
        CurrentMassKg -= fuelMass;
    }
    

    public static double GetMassOfFuelKg(double amountL, FuelType type)
    {
        return amountL * type.GetDensity();
    }
}