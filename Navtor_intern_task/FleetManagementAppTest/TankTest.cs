using FleetManagementApp;
using FleetManagementApp.Exceptions;

namespace FleetManagementAppTest;

public class TankTest
{

    [Theory]
    [InlineData(10, FuelType.Diesel, 5)]
    [InlineData(100, FuelType.HeavyFuel, 99)]
    [InlineData(200, FuelType.HeavyFuel, 200)]
    public void EmptyFully_ValidAmount_DoesNotThrowException(double maxCapacityL, FuelType type, double amountL)
    {
        var tank = new Tank(maxCapacityL, type);
        tank.Refuel(amountL);
        var exception = Record.Exception(() => tank.EmptyFully());
        Assert.Null(exception);
    }
    
    [Theory]
    [InlineData(10, FuelType.Diesel, 5, 2)]
    [InlineData(100, FuelType.HeavyFuel, 50, 49)]
    [InlineData(200, FuelType.HeavyFuel, 200, 1)]
    public void EmptyPartially_ValidAmount_DoesNotThrowException(
        double maxCapacity, FuelType type, double amountL, double amountToEmptyL)
    {
        var tank = new Tank(maxCapacity, type);
        tank.Refuel(amountL);
        var exception = Record.Exception(() => tank.EmptyPartially(amountToEmptyL));
        Assert.Null(exception);
    }

    
    [Theory]
    [InlineData(10, FuelType.Diesel, 10, 15)]
    [InlineData(100, FuelType.HeavyFuel, 50, 51)]
    public void EmptyPartially_InvalidAmount_ThrowsEmptyingTooMuchFuelException(
        double maxCapacityL, FuelType type, double amountL, double amountToEmptyL)
    {
        var tank = new Tank(maxCapacityL, type);
        tank.Refuel(amountL);
        Assert.Throws<EmptyingTooMuchFuelException>(
            () => tank.EmptyPartially(amountToEmptyL));
    }
    
    [Theory]
    [InlineData(10, FuelType.Diesel, 5)]
    [InlineData(100, FuelType.HeavyFuel, 50)]
    public void Refuel_ValidAmount_DoesNotThrowException(double maxCapacityL, FuelType type, double amountL)
    {
        var tank = new Tank(maxCapacityL, type);
        var exception = Record.Exception(() => tank.Refuel(amountL));
        Assert.Null(exception);
    }
    
    [Theory]
    [InlineData(10, FuelType.Diesel, 15)]
    [InlineData(100, FuelType.HeavyFuel, 150)]
    public void Refuel_InvalidAmount_ThrowsTankOverfillException(double maxCapacityL, FuelType type, double amountL)
    {
        var tank = new Tank(maxCapacityL, type);
        Assert.Throws<TankOverfillException>(() => tank.Refuel(amountL));
    }
    
}