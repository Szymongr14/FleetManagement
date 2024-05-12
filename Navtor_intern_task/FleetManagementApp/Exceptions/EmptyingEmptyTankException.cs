namespace FleetManagementApp.Exceptions;

public class EmptyingEmptyTankException : Exception
{
    public EmptyingEmptyTankException(string message) : base(message)
    {
    }
    
}