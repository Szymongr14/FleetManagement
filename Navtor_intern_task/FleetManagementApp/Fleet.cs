using System.ComponentModel.Design.Serialization;
using FleetManagementApp.Exceptions;

namespace FleetManagementApp;

public class Fleet
{
    public readonly HashSet<Ship> Ships = [];
    private string ShipOwner { get; set; }

    public Fleet(string shipOwner)
    {
        ValidateShipOwnerName(shipOwner);
        ShipOwner = shipOwner;
    }
    
    public void AddShip(Ship ship)
    {
        if (Ships.Any(existingShip => existingShip.Id == ship.Id))
        {
            throw new InvalidShipIdException(
                $"A ship with the ID {ship.Id} already exists in the fleet.");
        }
        Ships.Add(ship);
    }
    
    public static void ValidateShipOwnerName(string shipOwner)
    {
        if (string.IsNullOrEmpty(shipOwner))
        {
            throw new InvalidShipOwnerNameException(
                "The provided ship owner name is not valid. Please provide a valid name.");
        }
    }
    
    public void RemoveShip(string id)
    {
        var ship = CheckIfShipExist(id);
        Ships.Remove(ship);
    }
    
    private Ship CheckIfShipExist(string shipId)
    {
        return Ships.FirstOrDefault(ship => ship!.Id == shipId, null) ?? 
               throw new InvalidShipIdException("Ship not found");
    }
}