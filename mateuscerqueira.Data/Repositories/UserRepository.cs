using mateuscerqueira.Data.Context;
using mateuscerqueira.ToDoApp.Domain.Core;
using mateuscerqueira.ToDoApp.Domain.Core.Interfaces;
using mateuscerqueira.ToDoApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace mateuscerqueira.Data.Repositories;

public class UserRepository : PaginatedRepository<User, Guid>, IUserRepository
{
    protected readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    public async Task<PaginatedResult<User>> GetPaginatedAsync(
        int pageNumber = 1,
        int pageSize = 20,
        string? searchTerm = null,
        bool? isActive = null,
        CancellationToken cancellationToken = default)
    {
        Expression<Func<User, bool>>? filterExpression = null;

        // Construir expressão de filtro dinamicamente
        if (!string.IsNullOrEmpty(searchTerm) || isActive.HasValue)
        {
            filterExpression = user =>
                (string.IsNullOrEmpty(searchTerm) ||
                 user.Name.FirstName.Contains(searchTerm) ||
                 user.Name.LastName.Contains(searchTerm) ||
                 user.Email.Value.Contains(searchTerm)) &&
                (!isActive.HasValue || user.IsActive == isActive.Value);
        }

        return await base.GetPaginatedAsync(
            pageNumber,
            pageSize,
            user => user.Name.FirstName, // Ordenar por primeiro nome
            filterExpression,
            true,
            cancellationToken);
    }

}