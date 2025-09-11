using mateuscerqueira.Application.Security.Interfaces;
using mateuscerqueira.Application.Security.Services;
using mateuscerqueira.Data.Context;
using mateuscerqueira.ToDoApp.Domain.Core.Interfaces;
using mateuscerqueira.ToDoApp.Domain.Core.Uow;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data.Common;

namespace mateuscerqueira.Data.Uow;

internal class UnitOfWork(
    ApplicationDbContext context,
    ITokenService tokenService,
    IDataProtectionService dataProtectionService,
    IUserResolverService userResolverService,
    IUserRepository userRepository,
    IToDoItemRepository toDoItemRepository,
    PasswordService passwordService
    ) : IUnitOfWork
{
    private IDbContextTransaction _transaction;
    private bool _disposed;

    public ITokenService TokenService => tokenService;
    public IDataProtectionService DataProtectionService => dataProtectionService;
    public IUserResolverService UserResolverService => userResolverService;
    public IUserRepository UserRepository => userRepository;
    public IToDoItemRepository ToDoItemRepository => toDoItemRepository;
    public PasswordService PasswordService => passwordService;

    public bool Commit()
    {
        return context.SaveChanges() > 0;
    }
    public async Task<bool> CommitAsync(CancellationToken cancellationToken = default)
    {

        return await context.SaveChangesAsync() > 0;
    }
    public IEnumerable<string> DebugChanges()
    {
        throw new NotImplementedException();
    }

    public bool HasChanges()
    {
        return context.ChangeTracker.HasChanges();
    }
    public async Task<DbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        _transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        return _transaction.GetDbTransaction();
    }
    public async Task RollbackTrasactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction == null) return;
        await _transaction.RollbackAsync(cancellationToken);
    }
    public async Task CommitTransactioAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            if (_transaction != null)
            {
                await context.SaveChangesAsync(cancellationToken);
                await _transaction.CommitAsync(cancellationToken);
            }
        }
        catch
        {
            await RollbackTrasactionAsync(cancellationToken);
            throw;
        }
        finally
        {
            await DisposeTransactionAsync();
        }
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    private void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            return;
        }
        if (disposing)
        {
            _transaction.Dispose();
            context.Dispose();
        }
        _disposed = true;
    }
    private async Task DisposeTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }
    ~UnitOfWork()
    {
        Dispose(true);
    }
}
