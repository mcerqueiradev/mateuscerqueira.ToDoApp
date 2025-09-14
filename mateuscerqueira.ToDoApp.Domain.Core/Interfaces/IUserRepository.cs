using mateuscerqueira.ToDoApp.Domain.Entities;

namespace mateuscerqueira.ToDoApp.Domain.Core.Interfaces;

public interface IUserRepository : IRepository<User, Guid>
{
    Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<PaginatedResult<User>> GetPaginatedAsync(
        int pageNumber = 1,
        int pageSize = 20,
        string? searchTerm = null,
        bool? isActive = null,
        CancellationToken cancellationToken = default);
}
