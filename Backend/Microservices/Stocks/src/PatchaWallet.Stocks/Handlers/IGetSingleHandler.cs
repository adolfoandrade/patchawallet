using MediatR;

namespace PatchaWallet.Stocks
{
    public interface IGetSingleHandler<T> : IRequestHandler<GetSingleRequest<T>, T>
    { }
}
