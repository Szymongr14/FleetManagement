namespace FleetManagementApp.Exceptions;

public class InvalidShipMaxLoadException : Exception
{
    public InvalidShipMaxLoadException(string message) : base(message){}
}