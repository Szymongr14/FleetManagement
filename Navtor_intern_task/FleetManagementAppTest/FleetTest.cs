using FleetManagementApp;
using FleetManagementApp.Exceptions;

namespace FleetManagementAppTest;

public class FleetTest
{
    private static Ship CreateValidContainerShip()
    {
        return new ContainerShip("IMO9224764", 
            "test", 
            1, 1, 1, 
            new Position(new Coordinates(1, 1), 
                DateTime.Now.AddMilliseconds(-70)));
    }

    private static Ship CreateValidTankerShip()
    {
        return new TankerShip("IMO9451290", 
            "test", 
            1, 1, 100000, 
            new Position(new Coordinates(1, 89), 
                DateTime.Now.AddMilliseconds(-70))); 
    }
    
    [Fact]
    public void Check_Adding_Ship()
    {
        var fleet = new Fleet("Test");
        var ship = CreateValidContainerShip();
        fleet.AddShip(ship);
        Assert.Contains(ship, fleet.Ships);
    }
    
    [Fact]
    public void Check_Removing_Ship()
    {
        var fleet = new Fleet("Test");
        var ship = CreateValidContainerShip();
        fleet.AddShip(ship);
        fleet.RemoveShip(ship.Id);
        Assert.DoesNotContain(ship, fleet.Ships);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validate_Ship_Owner_Name(string name)
    {
        Assert.Throws<InvalidShipOwnerNameException>(
            () => Fleet.ValidateShipOwnerName(name));
    }
    
    [Fact]
    public void AddShip_AddingShipWithExistingId_ThrowsException()
    {
        var fleet = new Fleet("Test");
        var ship = CreateValidContainerShip();
        fleet.AddShip(ship);
        Assert.Throws<InvalidShipIdException>(
            () => fleet.AddShip(ship));
    }
}