namespace FleetManagementApp.Exceptions;

public class TankOverfillException : Exception
{
    public TankOverfillException(string message) : base(message)
    {
    }
}