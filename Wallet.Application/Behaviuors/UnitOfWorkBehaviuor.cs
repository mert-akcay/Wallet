using MediatR;
using System.Transactions;
using Wallet.Application.Helpers;
using Wallet.Infrastructure.DbContext;

namespace Wallet.Application.Behaviuors;

public sealed class UnitOfWorkBehavior<TRequest, TResponse>(ApplicationDbContext context)
 : IPipelineBehavior<TRequest, TResponse>
where TResponse : class
where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (IsNotCommand())
        {
            return await next();
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var response = await next(cancellationToken);

        try
        {
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception)
        {
            Type innerType = typeof(TResponse).GetGenericArguments()[0];

            var method = typeof(ResponseHelper)
                .GetMethod(nameof(ResponseHelper.Fail), [typeof(string), typeof(int)])!
                .MakeGenericMethod(innerType);

            var failResponse = method.Invoke(null, ["An error occurred while saving changes to the database.", 400]);

            return (TResponse)failResponse!;
        }
        finally
        {
            scope.Complete();
        }
        return response;
    }

    private static bool IsNotCommand()
    {
        return !typeof(TRequest).Name.EndsWith("Command");
    }
}