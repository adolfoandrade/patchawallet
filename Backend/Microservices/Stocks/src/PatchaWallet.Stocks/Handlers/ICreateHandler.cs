using MediatR;

namespace PatchaWallet.Stocks
{
    public interface ICreateHandler<T> : IRequestHandler<CreateRequest<T>, T>
    { }
}
