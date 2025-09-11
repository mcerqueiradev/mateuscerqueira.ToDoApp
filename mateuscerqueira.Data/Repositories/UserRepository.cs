using mateuscerqueira.Data.Context;
using mateuscerqueira.ToDoApp.Domain.Core.Interfaces;
using mateuscerqueira.ToDoApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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

}