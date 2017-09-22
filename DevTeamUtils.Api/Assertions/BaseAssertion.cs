using DevTeamUtils.Api.Notifications;

namespace DevTeamUtils.Api.Assertions
{
    public class BaseAssertion
    {
        public NotificationHandler Notifications { get; protected set; }

        public BaseAssertion()
        {
            Notifications = new NotificationHandler();
        }

        protected void SetNofication(string key, string value)
        {
            Notifications.Handle(key, value);
        }
    }
}
