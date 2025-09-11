using mateuscerqueira.ToDoApp.Domain.Core.Uow;
using MediatR;

namespace mateuscerqueira.Application.Common.Behaviours;

internal class UnitOfWorkBehaviour<TRequest, TReponse>(IUnitOfWork unitOfWork) : IPipelineBehavior<TRequest, TReponse> where TRequest : notnull
{
    public async Task<TReponse> Handle(TRequest request, RequestHandlerDelegate<TReponse> next, CancellationToken cancellationToken)
    {
        if (IsNotCommand())
        {
            return await next();
        }
        await unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            var result = await next();
            //unitOfWork.DebugChanges();

            await unitOfWork.CommitTransactioAsync(cancellationToken);
            return result;
        }
        catch (Exception)
        {
            await unitOfWork.RollbackTrasactionAsync(cancellationToken);
            throw;
        }
    }
    private static bool IsNotCommand()
    {
        return !typeof(TRequest).Name.EndsWith("Command");
    }
}