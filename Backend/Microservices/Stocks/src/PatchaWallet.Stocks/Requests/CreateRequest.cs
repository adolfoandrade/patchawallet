using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PatchaWallet.Stocks
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
