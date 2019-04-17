using MediatR;
using System;

namespace Patcha.InvestmentWallet.Api.Requests
{
    public class GetSingleRequest<T> : IRequest<T>
    {
        #region Properties
        public string Id { get; }
        #endregion

        #region Constructor
        public GetSingleRequest(string id)
        {
            if (String.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException(nameof(id));
            }

            Id = id;
        }
        #endregion
    }
}
