using FleetManagementApp.Exceptions;

namespace FleetManagementApp;

public class TankerShip : Ship
{
    private List<Tank> Tanks { get; } = [];

    public TankerShip(string id, string name, int widthM, int lengthM, double maxLoadKg, Position currentPosition) 
        : base(id, name, widthM, lengthM, maxLoadKg, currentPosition)
    {
    }
    
    public void InstallTanks(List<Tank> tanks)
    {
        foreach (var tank in tanks)
        {
            Tanks.Add(tank);
        }
    }
    
    public void AddFuel(Guid tankId, double amount, FuelType type)
    {
        var tank = CheckIfTankExists(tankId);

        var fuelMass = Tank.GetMassOfFuelKg(amount, type);
        if (fuelMass + CurrentLoadKg > MaxLoadKg)
        {
            throw new ShipOverloadingException(
                "The fuel cannot be added, adding fuel will cause ship overloading");
        }
        
        if (tank.Type != type) throw new InvalidFuelTypeException(
            "The provided fuel type does not match the tank's fuel type.");
        
        tank.Refuel(amount);
        CurrentLoadKg += fuelMass;
    }

    public void EmptyTankFully(Guid tankId)
    {
        var tank = Tanks.FirstOrDefault(tank => tank.Id == tankId);
        if (tank == null)
        {
            throw new InvalidTankIdException(
                "The tank cannot be emptied, the tank does not exist");
        }
        if (tank.CurrentCapacityL == 0)
        {
            throw new EmptyingEmptyTankException(
                "The tank cannot be emptied, the tank is already empty");
        }
        CurrentLoadKg -= tank.CurrentMassKg;
        tank.EmptyFully();
    }
    
    public void EmptyTankPartially(Guid tankId, double amount)
    {
        var tank = CheckIfTankExists(tankId);
        
        if (tank.CurrentCapacityL < amount)
        {
            throw new EmptyingTooMuchFuelException(
                "The tank cannot be emptied, the amount is greater than the current capacity");
        }
        
        tank.EmptyPartially(amount);
        var fuelMass = Tank.GetMassOfFuelKg(amount, tank.Type);
        CurrentLoadKg -= fuelMass;
    }

    public Tank CheckIfTankExists(Guid tankId)
    {
        var tank = Tanks.FirstOrDefault((tank => tank!.Id == tankId),null) ?? 
                   throw new InvalidTankIdException(
            $"The fuel cannot be added, the tank with id: {tankId} does not exist on this ship");
        return tank;
    }
}