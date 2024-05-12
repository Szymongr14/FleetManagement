using FleetManagementApp.Exceptions;

namespace FleetManagementApp;

public class Container
{
    public string Sender { get; }
    public string Addressee { get; }
    public string CargoDescription { get; }
    public double MassKg { get; }
    public Guid Id { get; }

    public Container(string sender, string addressee, string cargoDescription, double massKg)
    {
        ValidateMassKg(massKg);
        ValidateSenderAddresseeCargoDescription(sender, addressee, cargoDescription);
        
        Sender = sender;
        Addressee = addressee;
        CargoDescription = cargoDescription;
        MassKg = massKg;
        Id = Guid.NewGuid();
    }

    private static void ValidateSenderAddresseeCargoDescription(string sender, string addressee, string cargoDescription)
    {
        ArgumentNullException.ThrowIfNull(sender);
        ArgumentNullException.ThrowIfNull(addressee);
        ArgumentNullException.ThrowIfNull(cargoDescription);
        if (sender.Length < 2 || addressee.Length < 2 || cargoDescription.Length < 2)
        {
            throw new InvalidContainerDataException(
                "The provided data is not valid. Please provide valid data.");
        }
    }
    
    private static void ValidateMassKg(double massKg)
    {
        if (massKg <= 0)
        {
            throw new InvalidContainerMassException(
                "The mass of the container must be greater than 0");
        }
    }
}