using mateuscerqueira.ToDoApp.Domain.Entities;

namespace mateuscerqueira.ToDoApp.Domain.Core.Interfaces;

public interface IUserRepository : IRepository<User, Guid>
{
    Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken);

}
