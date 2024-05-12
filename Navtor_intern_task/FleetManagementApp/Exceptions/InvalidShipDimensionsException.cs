namespace FleetManagementApp.Exceptions;

public class InvalidShipDimensionsException : Exception
{
    public InvalidShipDimensionsException(string message) : base(message)
    {
    }
}