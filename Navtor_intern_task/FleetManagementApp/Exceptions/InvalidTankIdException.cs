namespace FleetManagementApp.Exceptions;

public class InvalidTankIdException : Exception
{
    public InvalidTankIdException(string message) : base(message)
    {
    }
}