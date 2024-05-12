namespace FleetManagementApp.Exceptions;

public class InvalidContainerIdException : Exception
{
    public InvalidContainerIdException(string message) : base(message)
    {
    }
}
