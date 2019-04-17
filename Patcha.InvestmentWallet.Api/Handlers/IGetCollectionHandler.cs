using MediatR;
using Patcha.InvestmentWallet.Api.Requests;
using System.Collections.Generic;

namespace Patcha.InvestmentWallet.Api.Handlers
{
    public interface IGetCollectionHandler<T> : IRequestHandler<GetCollectionRequest<T>, IEnumerable<T>>
    { }
}
