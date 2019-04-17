using MediatR;
using Patcha.InvestmentWallet.Api.Requests;

namespace Patcha.InvestmentWallet.Api.Handlers
{
    public interface IGetSingleHandler<T> : IRequestHandler<GetSingleRequest<T>, T>
    { }
}
