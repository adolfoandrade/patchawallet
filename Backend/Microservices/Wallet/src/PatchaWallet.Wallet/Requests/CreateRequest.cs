using MediatR;
using System;

namespace PatchaWallet.Wallet
{
    public class CreateRequest<T> : IRequest<T>
    {
        public T Item { get; }

        public CreateRequest(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            Item = item;
        }
    }
}
