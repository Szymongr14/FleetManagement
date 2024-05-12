namespace FleetManagementApp.Exceptions;

public class InvalidShipIdException : Exception
{
    public InvalidShipIdException(string message) : base(message)
    {
    }
}

