using MediatR;
using System;

namespace PatchaWallet.Stocks
{
    public class DeleteRequest<T> : IRequest
    {
        public T Item { get; }

        public DeleteRequest(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item)); ;
            }

            Item = item;
        }
    }
}
