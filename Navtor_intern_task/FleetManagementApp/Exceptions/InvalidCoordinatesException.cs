namespace FleetManagementApp.Exceptions;

public class InvalidCoordinatesException : Exception
{
    public InvalidCoordinatesException(string message) : base(message)
    {
    }
    
}