using MediatR;
using Patcha.InvestmentWallet.Api.Requests;

namespace Patcha.InvestmentWallet.Api.Handlers
{
    public interface ICreateHandler<T> : IRequestHandler<CreateRequest<T>, T>
    { }
}
