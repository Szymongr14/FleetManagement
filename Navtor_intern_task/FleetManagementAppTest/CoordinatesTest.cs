using FleetManagementApp;
using FleetManagementApp.Exceptions;

namespace FleetManagementAppTest;

public class CoordinatesTest
{
    [Theory]
    [InlineData(-91, 0)]
    [InlineData(91, 0)]
    [InlineData(0, -181)]
    [InlineData(0, 181)]
    public void Constructor_InvalidLatitudeAndLongitude_ThrowsInvalidCoordinatesException(double latitude, double longitude)
    {
        Assert.Throws<InvalidCoordinatesException>(() => new Coordinates(latitude, longitude));
    }
    
    [Theory]
    [InlineData(-90, 0)]
    [InlineData(90, 0)]
    [InlineData(0, -180)]
    [InlineData(0, 180)]
    public void Constructor_ValidLatitudeAndLongitude_DoesNotThrowException(double latitude, double longitude)
    {
        var exception = Record.Exception(() => new Coordinates(latitude, longitude));
        Assert.Null(exception);
    }
    
}