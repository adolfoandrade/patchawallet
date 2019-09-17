using MediatR;

namespace Patcha.InvestmentWallet.Api
{
    public interface ICreateHandler<T> : IRequestHandler<CreateRequest<T>, T>
    { }
}
