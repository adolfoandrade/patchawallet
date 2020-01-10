using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatchaWallet.Stocks
{
    public interface IDomainNotificationHandler<T>
    {
        bool HasNotifications { get; }
        IReadOnlyCollection<DomainNotification> Notifications { get; }
        void AddNotification(string key, string message);
        void AddNotification(DomainNotification notification);
        void AddNotifications(IReadOnlyCollection<DomainNotification> notifications);
        void AddNotifications(IList<DomainNotification> notifications);
        void AddNotifications(ICollection<DomainNotification> notifications);
    }
}
