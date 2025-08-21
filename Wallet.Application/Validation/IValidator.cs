namespace Wallet.Application.Validation;

public interface IValidator
{
    Task Validate<TEntity>(TEntity response, string errorParamVal);
    Task Validate<TEntity>(TEntity response, Func<TEntity, bool> predicate, string errorParamVal);
}
