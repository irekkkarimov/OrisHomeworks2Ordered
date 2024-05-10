namespace Domain.Entities.Abstractions;

public abstract class Entity
{
    public int Id { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is null)
            throw new NullReferenceException("Object to compare is null!");
        
        if (obj.GetType() != GetType())
            return false;

        return ((Entity)obj).Id == Id;
    }

    public override int GetHashCode()
    {
        return Id + GetType().GetHashCode();
    }
}