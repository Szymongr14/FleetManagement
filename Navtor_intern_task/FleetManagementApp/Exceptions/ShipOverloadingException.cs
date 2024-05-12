namespace FleetManagementApp.Exceptions;

public class ShipOverloadingException : Exception
{
    public ShipOverloadingException(string message) : base(message)
    {
    }
}