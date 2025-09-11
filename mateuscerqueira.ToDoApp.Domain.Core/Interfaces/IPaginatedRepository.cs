using mateuscerqueira.ToDoApp.Domain._Abstractions;
using System.Linq.Expressions;

namespace mateuscerqueira.ToDoApp.Domain.Core.Interfaces;

public interface IPaginatedRepository<T, TId> : IRepository<T, TId> where T : class, IEntity<TId>
{
    Task<PaginatedResult<T>> GetPaginatedAsync<TSortKey>(int pageNumber, int pageSize, Expression<Func<T, TSortKey>> sortExpression,
        Expression<Func<T, bool>>? filterExpression, bool ascending = true, CancellationToken cancellationToken = default);
}
