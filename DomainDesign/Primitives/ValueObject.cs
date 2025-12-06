namespace Odin.DomainDesign;

/// <summary>
/// Base class for value objects.
/// Implements value-based equality using a sequence of equality components.
/// </summary>
/// <remarks>
/// Value objects are immutable and compared by the values of their properties,
/// not by identity.
/// </remarks>
public abstract class ValueObject : IEquatable<ValueObject>
{
    /// <summary>
    /// Gets the components that participate in value-based equality.
    /// </summary>
    /// <returns>An ordered sequence of components to compare.</returns>
    protected abstract IEnumerable<object?> GetEqualityComponents();

    /// <inheritdoc />
    public override bool Equals(object? obj)
        => ReferenceEquals(this, obj) || (obj is ValueObject other && Equals(other));

    /// <inheritdoc />
    public bool Equals(ValueObject? other)
    {
        if (other is null || GetType() != other.GetType())
            return false;

        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;

            foreach (var component in GetEqualityComponents())
            {
                hash = hash * 23 + (component?.GetHashCode() ?? 0);
            }

            return hash;
        }
    }

    /// <summary>
    /// Determines whether two value objects are equal.
    /// </summary>
    public static bool operator ==(ValueObject? left, ValueObject? right)
        => Equals(left, right);

    /// <summary>
    /// Determines whether two value objects are not equal.
    /// </summary>
    public static bool operator !=(ValueObject? left, ValueObject? right)
        => !Equals(left, right);
}