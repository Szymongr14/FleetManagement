namespace FleetManagementApp.Exceptions;

public class InvalidShipOwnerNameException : Exception
{
    public InvalidShipOwnerNameException(string message) : base(message){}
}