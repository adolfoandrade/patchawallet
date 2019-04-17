using MediatR;
using Patcha.InvestmentWallet.Api.Requests;

namespace Patcha.InvestmentWallet.Api.Handlers
{
    public interface IDeleteHandler<T> : IRequestHandler<DeleteRequest<T>>
    { }
}
