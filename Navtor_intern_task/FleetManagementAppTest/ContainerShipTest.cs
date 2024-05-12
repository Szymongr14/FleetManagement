using FleetManagementApp;
using FleetManagementApp.Exceptions;

namespace FleetManagementAppTest;

public class ContainerShipTest
{
    [Fact]
    public void AddContainer_ValidContainer_AddsContainer()
    {
        var ship = CreateValidContainerShip();
        var container = CreateValidContainer();
        ship.AddContainer(container);
        Assert.Contains(container, ship.Containers);
    }
    
    [Fact]
    public void AddContainer_ValidContainer_IncreasesCurrentLoad()
    {
        var ship = CreateValidContainerShip();
        var container = CreateValidContainer();
        var shipLoadBefore = ship.CurrentLoadKg;
        ship.AddContainer(container);
        Assert.Equal(shipLoadBefore + container.MassKg, ship.CurrentLoadKg);
    }
    
    [Fact]
    public void AddContainer_ValidContainerMassOverCapacity_ThrowsContainerShipOverloadException()
    {
        var ship = CreateValidContainerShip();
        var container = new Container("test", "addressee", "desc", ship.MaxLoadKg + 1);
        Assert.Throws<ShipOverloadingException>(() => ship.AddContainer(container));
    }
    
    [Fact]
    public void RemoveContainer_ValidContainer_RemovesContainer()
    {
        var ship = CreateValidContainerShip();
        var container = CreateValidContainer();
        
        var shipLoadBefore = ship.CurrentLoadKg;
        ship.AddContainer(container);
        
        Assert.Equal(shipLoadBefore + container.MassKg, ship.CurrentLoadKg);
        
        ship.RemoveContainer(container.Id);
        Assert.DoesNotContain(container, ship.Containers);
    }
    
    [Fact]
    public void GetContainerById_ValidContainerId_ReturnsContainer()
    {
        var ship = CreateValidContainerShip();
        var container = CreateValidContainer();
        ship.AddContainer(container);
        var returnedContainer = ship.GetContainerById(container.Id);
        Assert.Equal(container, returnedContainer);
    }
    
    [Fact]
    public void GetContainerById_InvalidContainerId_ThrowsInvalidContainerIdException()
    {
        var ship = CreateValidContainerShip();
        var container = CreateValidContainer();
        ship.AddContainer(container);
        Assert.Throws<InvalidContainerIdException>(() => ship.GetContainerById(Guid.NewGuid()));
    }
    
    [Fact]
    public void RemoveContainer_ValidContainer_DecreasesCurrentLoad()
    {
        var ship = CreateValidContainerShip();
        var container = CreateValidContainer();
        ship.AddContainer(container);
        var shipLoadBefore = ship.CurrentLoadKg;
        ship.RemoveContainer(container.Id);
        Assert.Equal(shipLoadBefore - container.MassKg, ship.CurrentLoadKg);
    }
    

    private static ContainerShip CreateValidContainerShip()
    {
        return new ContainerShip("IMO9224764", "test", 1, 1, 100000, new Position(new Coordinates(1, 1), DateTime.Now));
    }

    private static Container CreateValidContainer()
    {
        return new Container("test", "addressee", "desc", 192);
    }
    
    
}