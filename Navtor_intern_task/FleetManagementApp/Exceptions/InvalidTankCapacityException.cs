namespace FleetManagementApp.Exceptions;

public class InvalidTankCapacityException : Exception
{
    public InvalidTankCapacityException(string message) : base(message)
    {
    }
}