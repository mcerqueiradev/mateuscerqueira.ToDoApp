using mateuscerqueira.Application.Security.Interfaces;
using mateuscerqueira.ToDoApp.Domain.Core.Interfaces;
using System.Data.Common;

namespace mateuscerqueira.ToDoApp.Domain.Core.Uow;

public interface IUnitOfWork
{
    public ITokenService TokenService { get; }
    public IUserResolverService UserResolverService { get; }
    public IDataProtectionService DataProtectionService { get; }
    public IUserRepository UserRepository { get; }
    public IToDoItemRepository ToDoItemRepository { get; }

    bool Commit();

    Task<bool> CommitAsync(CancellationToken cancellationToken = default);

    Task<DbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

    Task CommitTransactioAsync(CancellationToken cancellationToken = default);

    Task RollbackTrasactionAsync(CancellationToken cancellationToken = default);

    bool HasChanges();

    IEnumerable<string> DebugChanges();

}
