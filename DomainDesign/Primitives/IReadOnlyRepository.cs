using System.Linq.Expressions;

namespace Odin.DomainDesign;

/// <summary>
/// Defines the contract for a read-only repository for aggregate roots.
/// </summary>
/// <typeparam name="TAggregate">The type of the aggregate root.</typeparam>
/// <typeparam name="TId">The type of the aggregate identifier.</typeparam>
public interface IReadOnlyRepository<TAggregate, in TId>
    where TAggregate : class, IAggregateRoot<TId>
{
    /// <summary>
    /// Gets an aggregate by its identifier.
    /// </summary>
    /// <param name="id">The aggregate identifier.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>
    /// The aggregate instance if found; otherwise, <c>null</c>.
    /// </returns>
    Task<TAggregate?> GetByIdAsync(
        TId id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all aggregates.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A read-only list of aggregates.</returns>
    Task<IReadOnlyList<TAggregate>> ListAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets aggregates matching the specified predicate.
    /// </summary>
    /// <param name="predicate">The predicate to filter aggregates.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A read-only list of aggregates matching the predicate.</returns>
    Task<IReadOnlyList<TAggregate>> ListAsync(
        Expression<Func<TAggregate, bool>> predicate,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Determines whether any aggregate satisfies the specified predicate.
    /// </summary>
    /// <param name="predicate">The predicate to test aggregates.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>
    /// <c>true</c> if any aggregate matches the predicate; otherwise, <c>false</c>.
    /// </returns>
    Task<bool> AnyAsync(
        Expression<Func<TAggregate, bool>> predicate,
        CancellationToken cancellationToken = default);
}