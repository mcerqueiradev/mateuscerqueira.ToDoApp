using mateuscerqueira.ToDoApp.Domain.Entities;

namespace mateuscerqueira.ToDoApp.Domain.Core.Interfaces;

public interface IToDoItemRepository : IRepository<ToDoItem, Guid>
{
    Task<ToDoItem?> RetrieveAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ToDoItem?> RetrieveWithUserAsync(Guid id, CancellationToken cancellationToken = default);

    Task<PaginatedResult<ToDoItem>> GetPaginatedAsync(
        int pageNumber = 1,
        int pageSize = 20,
        string? searchTerm = null,
        bool? isCompleted = null,
        Guid? userId = null,
        CancellationToken cancellationToken = default);

    Task<PaginatedResult<ToDoItem>> GetByUserIdAsync(
        Guid userId,
        int pageNumber = 1,
        int pageSize = 20,
        string? searchTerm = null,
        bool? isCompleted = null,
        CancellationToken cancellationToken = default);
}
