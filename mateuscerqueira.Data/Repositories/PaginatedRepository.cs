using mateuscerqueira.Data.Context;
using mateuscerqueira.ToDoApp.Domain._Abstractions;
using mateuscerqueira.ToDoApp.Domain.Core;
using mateuscerqueira.ToDoApp.Domain.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace mateuscerqueira.Data.Repositories;

public abstract class PaginatedRepository<T, TId> : Repository<T, TId>, IPaginatedRepository<T, TId> where T : class, IEntity<TId>
{
    public PaginatedRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<PaginatedResult<T>> GetPaginatedAsync<TSortKey>(
        int pageNumber,
        int pageSize,
        Expression<Func<T, TSortKey>> sortExpression,
        Expression<Func<T, bool>>? filterExpression,
        bool ascending = true,
        CancellationToken cancellationToken = default)
    {
        var query = DbSet.AsNoTracking();

        if (filterExpression != null)
            query = query.Where(filterExpression);

        query = ascending ? query.OrderBy(sortExpression) : query.OrderByDescending(sortExpression);

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<T>(items, totalCount, pageNumber, pageSize);
    }
}
