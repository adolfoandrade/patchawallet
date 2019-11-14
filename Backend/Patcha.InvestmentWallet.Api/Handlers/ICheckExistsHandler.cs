using MediatR;

namespace Patcha.InvestmentWallet.Api
{
    public interface ICheckExistsHandler<T> : IRequestHandler<CheckExistsRequest<T>, T>
    { }
}
