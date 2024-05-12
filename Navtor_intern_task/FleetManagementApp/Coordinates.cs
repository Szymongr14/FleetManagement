using FleetManagementApp.Exceptions;

namespace FleetManagementApp;

public record Coordinates
{
    public readonly double Latitude;
    public readonly double Longitude;
    
    public Coordinates(double latitude, double longitude)
    {
        if (latitude < -90 || latitude > 90)
        {
            throw new InvalidCoordinatesException("Latitude must be between -90 and 90");
        }

        if (longitude < -180 || longitude > 180)
        {
            throw new InvalidCoordinatesException("Longitude must be between -180 and 180");
        }

        Latitude = latitude;
        Longitude = longitude;
    }
}