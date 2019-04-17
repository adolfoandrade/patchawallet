using MediatR;
using Patcha.InvestmentWallet.Api.Requests;

namespace Patcha.InvestmentWallet.Api.Handlers
{
    public interface ICheckExistsHandler<T> : IRequestHandler<CheckExistsRequest<T>, bool>
    { }
}
