using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatchaWallet.Stocks
{
    public interface IGetCollectionHandler<T> : IRequestHandler<GetCollectionRequest<T>, IEnumerable<T>>
    { }
}
