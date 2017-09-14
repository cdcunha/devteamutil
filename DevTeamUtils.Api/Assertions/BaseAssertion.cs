using DevTeamUtils.Api.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
