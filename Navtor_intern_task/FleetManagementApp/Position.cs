namespace FleetManagementApp;

public record Position
{
    public readonly Coordinates Coordinates;
    public DateTime RecordTime;
    public DateTime GPSTime;
    public Position(Coordinates coordinates, DateTime gpsTime)
    {
        Coordinates = coordinates;
        RecordTime = DateTime.Now;
        GPSTime = gpsTime;
    }
}