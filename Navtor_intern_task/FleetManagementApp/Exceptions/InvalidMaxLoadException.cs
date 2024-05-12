namespace FleetManagementApp.Exceptions;

public class InvalidMaxLoadException : Exception
{
    public InvalidMaxLoadException(string message) : base(message)
    {
    }
    
}