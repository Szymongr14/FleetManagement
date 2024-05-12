using FleetManagementApp;
using FleetManagementApp.Exceptions;

namespace FleetManagementAppTest;

public class ShipTest
{
    [Theory]
    [InlineData("test")]
    [InlineData("IMO9224761")]
    [InlineData("MO9224761")]
    [InlineData("IMO9224762x")]
    [InlineData("IMO9 224762x")]
    [InlineData("")]
    public void Constructor_InvalidId_ThrowsInvalidShipIdException(string id)
    {
        Assert.Throws<InvalidShipIdException>(() => Ship.ValidateShipId(id));
    }

    [Theory]
    [InlineData("IMO9224764")]
    [InlineData("IMO9829930")]
    public void Constructor_ValidId_DoesNotThrowException(string id)
    {
        var exception = Record.Exception(() => Ship.ValidateShipId(id));
        Assert.Null(exception);
    }
    
    [Fact]
    public void Constructor_ValidName_DoesNotThrowException()
    {
        const string validName = "test";
        var exception = Record.Exception(() => Ship.ValidateName(validName));
        Assert.Null(exception);
    }
    
    
    [Theory]
    [InlineData("t")]
    [InlineData("1")]
    public void Constructor_InvalidName_ThrowsInvalidShipNameException(string name)
    {
        Assert.Throws<InvalidShipNameException>(() => Ship.ValidateName(name));
    }
    
    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 0)]
    [InlineData(0, 1)]
    [InlineData(-1, 1)]
    public void Constructor_InvalidWidthAndLength_ThrowsInvalidShipDimensionsException(int width, int length)
    {
        Assert.Throws<InvalidShipDimensionsException>(() => Ship.ValidateWidthAndLength(width, length));
    }
    
    [Theory]
    [InlineData(1, 1)]
    [InlineData(1, 2)]
    [InlineData(2, 1)]
    public void Constructor_ValidWidthAndLength_DoesNotThrowException(int width, int length)
    {
        var exception = Record.Exception(() => Ship.ValidateWidthAndLength(width, length));
        Assert.Null(exception);
    }
    
    [Fact]
    public void UpdatingCurrentPosition_UpdatesPosition()
    {
        var ship = CreateValidContainerShip();
        var newPosition = new Position(new Coordinates(2, 2), DateTime.Now);
        ship.UpdatePosition(newPosition);
        Assert.Equal(newPosition, ship.GetCurrentPosition());
    }
    
    [Fact]
    public void UpdatingCurrentPositionsFewTimes_UpdatesPositions()
    {
        var ship = CreateValidContainerShip();
        var newPosition1 = new Position(new Coordinates(2, 2), DateTime.Now.AddMilliseconds(-70));
        var newPosition2 = new Position(new Coordinates(3, 3), DateTime.Now.AddMilliseconds(-70));
        var newPosition3 = new Position(new Coordinates(3, 4), DateTime.Now.AddMilliseconds(-70));
        ship.UpdatePosition(newPosition1);
        ship.UpdatePosition(newPosition2);
        ship.UpdatePosition(newPosition3);
        Assert.Equal(newPosition3, ship.GetCurrentPosition());
    }
    
    [Fact]
    public void HashCode_SameId_ReturnsSameHashCode()
    {
        var ship1 = CreateValidContainerShip();
        var ship2 = CreateValidContainerShip();
        Assert.Equal(ship1.GetHashCode(), ship2.GetHashCode());
    }
    
    private static ContainerShip CreateValidContainerShip()
    {
        return new ContainerShip("IMO9224764", "test", 1, 1, 1, new Position(new Coordinates(1, 1), DateTime.Now));
    }
}
    
    