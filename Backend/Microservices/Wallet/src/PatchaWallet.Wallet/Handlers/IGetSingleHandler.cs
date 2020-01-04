using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatchaWallet.Wallet
{
    public interface IGetSingleHandler<T> : IRequestHandler<GetSingleRequest<T>, T>
    { }
}
