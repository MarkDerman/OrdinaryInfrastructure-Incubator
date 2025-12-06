namespace Odin.DomainDesign;

/// <summary>
/// Represents a domain event that occurred within the domain model.
/// </summary>
public interface IDomainEvent
{
    /// <summary>
    /// Gets the UTC date and time when the domain event occurred.
    /// </summary>
    DateTime OccurredOnUtc { get; }
    
    /// <summary>
    /// Indicates whether the event has been published.
    /// </summary>
    public bool IsPublished { get; }
}

/// <summary>
/// Base record type for domain events.
/// Sets <see cref="OccurredOnUtc"/> to the current UTC date and time by default.
/// </summary>
public abstract record DomainEvent : IDomainEvent
{
    /// <inheritdoc />
    public DateTime OccurredOnUtc { get; init; } = DateTime.UtcNow;
    
    /// <inheritdoc />
    public bool IsPublished { get; set; }
}