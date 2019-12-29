using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatchaWallet.Stocks.Handlers
{
    public interface IGetCollectionHandler<T> : IRequestHandler<GetCollectionRequest<T>, IEnumerable<T>>
    { }
}
