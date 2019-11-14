using MediatR;

namespace Patcha.InvestmentWallet.Api
{
    public interface IUpdateHandler<T> : IRequestHandler<UpdateRequest<T>, T>
    { }
}
