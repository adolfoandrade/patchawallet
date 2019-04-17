using MediatR;
using Patcha.InvestmentWallet.Api.Requests;

namespace Patcha.InvestmentWallet.Api.Handlers
{
    public interface IUpdateHandler<T> : IRequestHandler<UpdateRequest<T>, T>
    { }
}
