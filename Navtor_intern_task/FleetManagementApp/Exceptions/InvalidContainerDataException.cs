namespace FleetManagementApp.Exceptions;

public class InvalidContainerDataException : Exception
{
    public InvalidContainerDataException(string message) : base(message)
    {
    }
}