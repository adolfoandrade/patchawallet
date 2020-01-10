using MediatR;

namespace PatchaWallet.Stocks
{
    public interface IDeleteHandler<T> : IRequestHandler<DeleteRequest<T>>
    { }
}
