using MediatR;
using System.Collections.Generic;

namespace Patcha.InvestmentWallet.Api
{
    public class GetCollectionRequest<T> : IRequest<IEnumerable<T>>
    { }
}
