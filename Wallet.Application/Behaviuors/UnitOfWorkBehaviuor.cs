using MediatR;
using System.Transactions;
using Wallet.Infrastructure.UnitOfWork;

namespace Wallet.Application.Behaviuors;

public sealed class UnitOfWorkBehavior<TRequest, TResponse>(IUnityOfWork unitOfWork)
 : IPipelineBehavior<TRequest, TResponse>
where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (IsNotCommand())
        {
            return await next();
        }

        try
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var response = await next();


                await unitOfWork.SaveChangesAsync(cancellationToken);
                scope.Complete();

                return response;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return await next();
        }
    }

    private static bool IsNotCommand()
    {
        return !typeof(TRequest).Name.EndsWith("Command");
    }
}