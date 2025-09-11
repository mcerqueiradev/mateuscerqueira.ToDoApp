using mateuscerqueira.Data.Context;
using mateuscerqueira.ToDoApp.Domain.Core.Interfaces;
using mateuscerqueira.ToDoApp.Domain.Entities;

namespace mateuscerqueira.Data.Repositories;

public class ToDoItemRepository : PaginatedRepository<ToDoItem, Guid>, IToDoItemRepository
{
    protected readonly ApplicationDbContext _context;

    public ToDoItemRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}