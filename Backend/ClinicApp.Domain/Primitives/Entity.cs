using System;

namespace ClinicApp.Domain.Primitives;

public abstract class Entity<TId> : IEquatable<Entity<TId>>
    where TId : ValueObject
{
    protected Entity(TId id)
    {
        Id = id;
    }
    
    protected Entity()
    {
    }


    public TId Id { get; private init; }

    public static bool operator ==(Entity<TId>? left, Entity<TId>? right)
    {
        if (ReferenceEquals(left, right))
        {
            return true;
        }

        return left is not null && right is not null && left.Equals(right);
    }


    public static bool operator !=(Entity<TId>? left, Entity<TId>? right)
    {
        return !(left == right);
    }

    public bool Equals(Entity<TId>? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Id.Equals(other.Id);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        return Equals((Entity<TId>)obj);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode() * 37;
    }
}
