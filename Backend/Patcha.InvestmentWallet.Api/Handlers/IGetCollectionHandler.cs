using MediatR;
using System.Collections.Generic;

namespace Patcha.InvestmentWallet.Api
{
    public interface IGetCollectionHandler<T> : IRequestHandler<GetCollectionRequest<T>, IEnumerable<T>>
    { }
}
