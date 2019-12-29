using MediatR;

namespace PatchaWallet.Wallet
{
    public interface ICreateHandler<T> : IRequestHandler<CreateRequest<T>, T>
    { }
}
