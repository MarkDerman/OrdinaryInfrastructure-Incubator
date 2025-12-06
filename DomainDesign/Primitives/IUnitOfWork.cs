namespace Odin.DomainDesign;

/// <summary>
/// Represents a unit of work over one or more repositories.
/// </summary>
/// <remarks>
/// The unit of work encapsulates atomic changes across multiple aggregates,
/// typically mapping to a single database transaction.
/// </remarks>
public interface IUnitOfWork
{
    /// <summary>
    /// Persists all changes made in the current unit of work.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>
    /// The number of state entries written to the underlying store.
    /// </returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}