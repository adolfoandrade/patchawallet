using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatchaWallet.Wallet
{
    public class GetCollectionRequest<T> : IRequest<IEnumerable<T>>
    {
        public int PageSize { get; }
        public int NextCursor { get; }
        public string Search { get; }

        public GetCollectionRequest()
        {
        }

        public GetCollectionRequest(int pageSize = 10, int nextCursor = 0, string search = "")
        {
            PageSize = pageSize;
            NextCursor = nextCursor;
            Search = search;
        }
    }
}
