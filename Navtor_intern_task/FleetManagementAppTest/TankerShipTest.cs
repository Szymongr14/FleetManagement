using FleetManagementApp;
using FleetManagementApp.Exceptions;

namespace FleetManagementAppTest;

public class TankerShipTest
{
    private readonly TankerShip _ship;
    private readonly Tank _tank;
    
    public TankerShipTest()
    {
        _ship = CreateValidTankerShip();
        _tank = CreateValidDieselTank();
        _ship.InstallTanks([_tank]);
    }
    
    [Fact]
    public void Equals_SameId_ReturnsTrue()
    {
        var ship1 = CreateValidTankerShip();
        var ship2 = CreateValidTankerShip();
        Assert.True(ship1.Equals(ship2));
    }
    
    [Fact]
    public void CheckIfTankExists_TankExists_DoesNotThrowException()
    {
        var exception =  Record.Exception(() => _ship.CheckIfTankExists(_tank.Id));
        Assert.Null(exception);
    }
    
    
    [Fact]
    public void CheckIfTankExists_TankDoesNotExist_ThrowsException()
    {
        Assert.Throws<InvalidTankIdException>(() => _ship.CheckIfTankExists(Guid.NewGuid()));
    }
    
    [Fact]
    public void EmptyTankFully_ValidTank_EmptiesTank()
    {
        _ship.AddFuel(_tank.Id, 100, FuelType.Diesel);
        _ship.EmptyTankFully(_tank.Id);
        Assert.Equal(0, _ship.CurrentLoadKg);
    }
    
    [Fact]
    public void EmptyTankFully_TankDoesNotExist_ThrowsException()
    {
        Assert.Throws<InvalidTankIdException>(() => 
            _ship.EmptyTankFully(Guid.NewGuid()));
    }
    
    [Fact]
    public void EmptyTankFully_TankIsEmpty_ThrowsException()
    {
        Assert.Throws<EmptyingEmptyTankException>( () => _ship.EmptyTankFully(_tank.Id));
    }
    
    [Theory]
    [InlineData(50, FuelType.Diesel, 40)]
    [InlineData(50, FuelType.Diesel, 1)]
    public void EmptyTankPartially_ValidTank_DecreaseLoad(double amountL, FuelType type, double amountToEmptyL)
    {
        _ship.AddFuel(_tank.Id, amountL, type);
        var shipLoadBeforeKg = _ship.CurrentLoadKg;
        _ship.EmptyTankPartially(_tank.Id, amountToEmptyL);
        var massOfFuelKg = Tank.GetMassOfFuelKg(amountToEmptyL, type);
        var expectedShipLoadKg = shipLoadBeforeKg - massOfFuelKg;
        Assert.Equal(expectedShipLoadKg, _ship.CurrentLoadKg);
    }
    
    private static TankerShip CreateValidTankerShip()
    {
        return new TankerShip("IMO9224764", 
            "test", 1, 1, 
            1000000, 
            new Position(new Coordinates(1, 1), DateTime.Now.AddMilliseconds(-70)));
    }
    
    private static Tank CreateValidDieselTank()
    {
        return new Tank(100012, FuelType.Diesel);
    }
}
    