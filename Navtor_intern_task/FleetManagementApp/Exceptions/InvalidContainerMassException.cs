namespace FleetManagementApp.Exceptions;

public class InvalidContainerMassException : Exception
{
    public InvalidContainerMassException(string message) : base(message)
    {
    }
}