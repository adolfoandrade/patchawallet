using MediatR;

namespace Patcha.InvestmentWallet.Api
{
    public interface IGetSingleHandler<T> : IRequestHandler<GetSingleRequest<T>, T>
    { }
}
