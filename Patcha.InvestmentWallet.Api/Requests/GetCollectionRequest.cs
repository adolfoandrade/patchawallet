using MediatR;
using System.Collections.Generic;

namespace Patcha.InvestmentWallet.Api.Requests
{
    public class GetCollectionRequest<T> : IRequest<IEnumerable<T>>
    { }
}
