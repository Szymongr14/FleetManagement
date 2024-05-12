namespace FleetManagementApp.Exceptions;

public class InvalidShipNameException : Exception
{
    public InvalidShipNameException(string message) : base(message)
    {
    }
    
}