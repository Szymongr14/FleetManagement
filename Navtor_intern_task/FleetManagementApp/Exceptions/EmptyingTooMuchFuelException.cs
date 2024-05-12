namespace FleetManagementApp.Exceptions;

public class EmptyingTooMuchFuelException : Exception
{
    public EmptyingTooMuchFuelException(string message) : base(message)
    {
    }
    
}