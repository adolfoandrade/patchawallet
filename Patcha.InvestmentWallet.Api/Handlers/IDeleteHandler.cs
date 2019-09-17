using MediatR;

namespace Patcha.InvestmentWallet.Api
{
    public interface IDeleteHandler<T> : IRequestHandler<DeleteRequest<T>>
    { }
}
