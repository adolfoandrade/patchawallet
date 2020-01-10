using MediatR;

namespace PatchaWallet.Stocks
{
    public interface ICheckExistsHandler<T> : IRequestHandler<CheckExistsRequest<T>, T>
    { }
}
