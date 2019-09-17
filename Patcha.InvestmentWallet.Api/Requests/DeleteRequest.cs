using MediatR;
using System;

namespace Patcha.InvestmentWallet.Api
{
    public class DeleteRequest<T> : IRequest
    {
        #region Properties
        public T Item { get; }
        #endregion

        #region Constructor
        public DeleteRequest(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item)); ;
            }

            Item = item;
        }
        #endregion
    }
}
