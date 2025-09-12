using mateuscerqueira.Data.Context;
using mateuscerqueira.ToDoApp.Domain.Core;
using mateuscerqueira.ToDoApp.Domain.Core.Interfaces;
using mateuscerqueira.ToDoApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace mateuscerqueira.Data.Repositories;

public class ToDoItemRepository : PaginatedRepository<ToDoItem, Guid>, IToDoItemRepository
{
    protected readonly ApplicationDbContext _context;

    public ToDoItemRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ToDoItem?> RetrieveWithUserAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.ToDoItems
            .Include(t => t.User)
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<PaginatedResult<ToDoItem>> GetPaginatedAsync(
        int pageNumber = 1,
        int pageSize = 20,
        string? searchTerm = null,
        bool? isCompleted = null,
        Guid? userId = null,
        CancellationToken cancellationToken = default)
    {
        var query = _context.ToDoItems
            .Include(t => t.User) 
            .AsQueryable();

        // Aplicar filtros
        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(t =>
                t.Title.Contains(searchTerm) ||
                (t.Description != null && t.Description.Contains(searchTerm)));
        }

        if (isCompleted.HasValue)
        {
            query = query.Where(t => t.IsCompleted == isCompleted.Value);
        }

        if (userId.HasValue)
        {
            query = query.Where(t => t.UserId == userId.Value);
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderByDescending(t => t.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<ToDoItem>(items, totalCount, pageNumber, pageSize);
    }

    public async Task<PaginatedResult<ToDoItem>> GetByUserIdAsync(
    Guid userId,
    int pageNumber = 1,
    int pageSize = 20,
    string? searchTerm = null,
    bool? isCompleted = null,
    CancellationToken cancellationToken = default)
    {
        var query = _context.ToDoItems
            .Where(t => t.UserId == userId)
            .AsQueryable();

        // Filtros adicionais
        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(t =>
                t.Title.Contains(searchTerm) ||
                (t.Description != null && t.Description.Contains(searchTerm)));
        }

        if (isCompleted.HasValue)
        {
            query = query.Where(t => t.IsCompleted == isCompleted.Value);
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderByDescending(t => t.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<ToDoItem>(items, totalCount, pageNumber, pageSize);
    }
}