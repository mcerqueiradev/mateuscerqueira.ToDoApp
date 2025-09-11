using mateuscerqueira.ToDoApp.Domain.Entities;

namespace mateuscerqueira.ToDoApp.Domain.Core.Interfaces;

public interface IToDoItemRepository : IRepository<ToDoItem, Guid>
{
}
