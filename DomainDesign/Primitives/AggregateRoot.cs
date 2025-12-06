namespace Odin.DomainDesign;

/// <summary>
/// Marker interface for aggregate roots. Distinguishes domain entities that are aggregate roots,
/// as opposed to child entities of an aggregate root, value types or other....
/// </summary>
/// <typeparam name="TId">The type of the aggregate identifier.</typeparam>
/// <remarks>
/// An aggregate root is the entry point to a consistency boundary in the domain model.
/// </remarks>
public interface IAggregateRoot<out TId>
{
    /// <summary>
    /// Gets the aggregate root entity identifier.
    /// </summary>
    TId Id { get; }
}


/// <summary>
/// Base class for aggregate roots.
/// Provides support for domain events in addition to entity identity.
/// </summary>
/// <typeparam name="TId">The type of the aggregate identifier.</typeparam>
public abstract class AggregateRoot<TId> : IAggregateRoot<TId>, IEquatable<AggregateRoot<TId>>
{
    /// <summary>
    /// Gets the entity identifier.
    /// </summary>
    public TId Id { get; protected set; } = default!;

    /// <inheritdoc />
    public override bool Equals(object? obj)
        => ReferenceEquals(this, obj) || (obj is AggregateRoot<TId> other && Equals(other));

    /// <inheritdoc />
    public bool Equals(AggregateRoot<TId>? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        if (GetType() != other.GetType()) return false;

        // Entities without IDs (default value) are not considered equal.
        if (EqualityComparer<TId>.Default.Equals(Id, default!)) return false;
        if (EqualityComparer<TId>.Default.Equals(other.Id, default!)) return false;

        return EqualityComparer<TId>.Default.Equals(Id, other.Id);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        // Combine runtime type and Id to avoid collisions across hierarchies.
        // ReSharper disable once NonReadonlyMemberInGetHashCode
        return HashCode.Combine(GetType(), Id);
    }

    /// <summary>
    /// Determines whether two entities are equal.
    /// </summary>
    public static bool operator ==(AggregateRoot<TId>? left, AggregateRoot<TId>? right)
        => Equals(left, right);

    /// <summary>
    /// Determines whether two entities are not equal.
    /// </summary>
    public static bool operator !=(AggregateRoot<TId>? left, AggregateRoot<TId>? right)
        => !Equals(left, right);
    
    private readonly List<IDomainEvent> _domainEvents = new();

    /// <summary>
    /// Gets the domain events that have been raised by this aggregate.
    /// </summary>
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    /// <summary>
    /// Adds a domain event to the aggregate's event collection.
    /// </summary>
    /// <param name="domainEvent">The domain event to raise.</param>
    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    /// <summary>
    /// Clears all domain events that have been raised by this aggregate.
    /// Typically called after events have been dispatched.
    /// </summary>
    public void ClearDomainEvents() => _domainEvents.Clear();
}