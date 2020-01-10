using MediatR;
using System;

namespace PatchaWallet.Stocks
{
    public class CheckExistsRequest<T> : IRequest<T>
    {
        public T Item { get; }

        public CheckExistsRequest(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            Item = item;
        }
    }
}
