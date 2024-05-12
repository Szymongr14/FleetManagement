using System.Text.RegularExpressions;
using FleetManagementApp.Exceptions;

namespace FleetManagementApp;

public abstract partial class Ship
{
    public string Id { get;}
    protected string Name { get;}
    protected int WidthM { get;}
    protected int LengthM { get;}
    public double MaxLoadKg { get; }
    public double CurrentLoadKg { get; protected set; }

    private readonly LinkedList<Position> _positions = [];
    
    protected Ship(string id, string name, int widthM, int lengthM, double maxLoadKg , Position currentPosition)
    {
        ValidateShipId(id);
        ValidateWidthAndLength(widthM, lengthM);
        ValidateName(name);
        ValidatePosition(currentPosition);
        ValidateMaxLoadKg(maxLoadKg);

        Id = id;
        WidthM = widthM;
        LengthM = lengthM;
        Name = name;
        MaxLoadKg = maxLoadKg;
        CurrentLoadKg = 0;
        _positions.AddLast(currentPosition);
    }
    
    public static void ValidateWidthAndLength(int width, int length)
    {
        if (width <= 0 || length <= 0)
        {
            throw new InvalidShipDimensionsException(
                "The provided dimensions are not valid. Please provide valid dimensions.");
        }
    }
    
    public static void ValidateName(string name)
    {
        ArgumentNullException.ThrowIfNull(name);
        if (name.Length < 2)
        {
            throw new InvalidShipNameException(
                "The provided name is not valid. Please provide a valid name.");
        }
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType()!= obj.GetType())
            return false;

        var other = (Ship)obj;
        return Id == other.Id;
    }

    public Position GetCurrentPosition()
    {
        return _positions.Last!.Value;
    }

    public void UpdatePosition(Position newPosition)
    {
        _positions.AddLast(newPosition);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public static void ValidateShipId(string id)
    {
        if (RegexForIdWithImo().IsMatch(id))
        {
            var digits = id.Remove(0, 3);
            if (!CheckImoSum(digits))
            {
                throw new InvalidShipIdException("The provided ID is not valid. Please provide a valid ID.");
            }
        }
        else
        {
            throw new InvalidShipIdException("The provided ID is not valid. Please provide a valid ID.");
        }
    }

    private static void ValidatePosition(Position position)
    {
        ArgumentNullException.ThrowIfNull(position);
        ArgumentNullException.ThrowIfNull(position.Coordinates);
        ArgumentNullException.ThrowIfNull(position.RecordTime);
    }

    private static void ValidateMaxLoadKg(double maxLoadKg)
    {
        if (maxLoadKg <= 0)
        {
            throw new InvalidShipMaxLoadException(
                "The provided ship max load value are not valid. Please provide valid value.");
        }
    }

    private static bool CheckImoSum(string digits)
    {
        var sum = 0;
        var multiplier = 7;
        for(int i = 0; i < digits.Length - 1; i++)
        {
            sum += multiplier * int.Parse(digits[i].ToString()) ;
            multiplier--;
        }
        return (sum % 10) == int.Parse(digits[^1].ToString());
    }

    [GeneratedRegex(@"^IMO\d{7}$")]
    private static partial Regex RegexForIdWithImo();
}