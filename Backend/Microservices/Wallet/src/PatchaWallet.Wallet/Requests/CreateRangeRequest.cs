using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PatchaWallet.Wallet
{
    public class CreateRangeRequest<T> : IRequest<T>
    {
        public IEnumerable<T> Items { get; }

        public CreateRangeRequest(IEnumerable<T> items)
        {
            if (items == null || !items.Any())
            {
                throw new ArgumentNullException(nameof(items));
            }

            Items = items;
        }
    }
}
