using MediatR;

namespace PatchaWallet.Stocks
{
    public interface IUpdateHandler<T> : IRequestHandler<UpdateRequest<T>, T>
    { }
}
