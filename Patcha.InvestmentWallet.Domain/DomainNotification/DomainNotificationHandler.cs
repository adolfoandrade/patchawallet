using System.Collections.Generic;
using System.Linq;

namespace Patcha.InvestmentWallet.Domain.DomainNotification
{
    public class DomainNotificationHandler : IDomainNotificationHandler<DomainNotification>
    {
        private readonly List<DomainNotification> _notifications;
        public IReadOnlyCollection<DomainNotification> Notifications => _notifications;
        public bool HasNotifications => _notifications.Any();

        public DomainNotificationHandler()
        {
            _notifications = new List<DomainNotification>();
        }

        public void AddNotification(string key, string message)
        {
            _notifications.Add(new DomainNotification(key, message));
        }

        public void AddNotification(DomainNotification notification)
        {
            _notifications.Add(notification);
        }

        public void AddNotifications(IReadOnlyCollection<DomainNotification> notifications)
        {
            _notifications.AddRange(notifications);
        }

        public void AddNotifications(IList<DomainNotification> notifications)
        {
            _notifications.AddRange(notifications);
        }

        public void AddNotifications(ICollection<DomainNotification> notifications)
        {
            _notifications.AddRange(notifications);
        }
    }
}
