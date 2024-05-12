using FleetManagementApp;
using FleetManagementApp.Exceptions;

namespace FleetManagementAppTest;

public class ContainerTest
{
    private const string ValidAddressee = "addressee";
    private const string ValidSender = "sender";
    private const string ValidCargoDescription = "cargoDescription";
    private const double ValidMassKg = 123.90;
    
    [Theory]
    [InlineData(123232)]
    [InlineData(32.32)]
    public void Constructor_ValidMassKg_ReturnsCorrectMassKg(double massKg)
    {
        var container = new Container(ValidSender, ValidAddressee, ValidCargoDescription, massKg);
        Assert.Equal(massKg, container.MassKg);
    }
    
    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void Constructor_InvalidMassKg_ThrowsInvalidContainerMassException(double massKg)
    {
        Assert.Throws<InvalidContainerMassException>(
            () => new Container(ValidSender, ValidAddressee, ValidCargoDescription, massKg));
    }
    
    [Theory]
    [InlineData("aa", "bc", "ca")]
    [InlineData("test", "test", "test")]
    public void Constructor_ValidSenderAddresseeCargoDescription_ReturnsCorrectData(
        string sender, string addressee, string cargoDescription)
    {
        var container = new Container(sender, addressee, cargoDescription, ValidMassKg);
        Assert.Equal(sender, container.Sender);
        Assert.Equal(addressee, container.Addressee);
        Assert.Equal(cargoDescription, container.CargoDescription);
    }
    
    [Theory]
    [InlineData("a", "ba", "ca")]
    [InlineData("aa", "a", "aa")]
    [InlineData("bd", "ba", "d")]
    public void Constructor_InvalidSenderAddresseeCargoDescription_ThrowsInvalidContainerDataException(
        string sender, string addressee, string cargoDescription)
    {
        Assert.Throws<InvalidContainerDataException>(
            () => new Container(sender, addressee, cargoDescription, ValidMassKg));
    }
    
}