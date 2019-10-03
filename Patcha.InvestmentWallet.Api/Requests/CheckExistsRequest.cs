using MediatR;
using System;

namespace Patcha.InvestmentWallet.Api
{
    public class CheckExistsRequest<T> : IRequest<T>
    {
        #region Properties
        public T Item { get; }
        #endregion

        #region Constructor
        public CheckExistsRequest(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            Item = item;
        }
        #endregion
    }
}
