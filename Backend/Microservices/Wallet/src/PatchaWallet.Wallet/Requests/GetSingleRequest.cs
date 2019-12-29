using MediatR;
using System;

namespace PatchaWallet.Wallet
{
    public class GetSingleRequest<T> : IRequest<T>
    {
        public string Id { get; }

        public GetSingleRequest(string id)
        {
            if (String.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException(nameof(id));
            }

            Id = id;
        }
    }
}
