using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouchDevUltimate.Systems.Notification
{
    public class Notification
    {
        public string name;

        private List<(object subscriber, Action<object, EventArgs> action)> subscribers;
        private object owner;

        public Notification(object owner, string name)
        {
            this.owner = owner;
            this.name = name;
            subscribers = new List<(object subscriber, Action<object, EventArgs> action)>();
        }

        public void Subscribe(object subscriber, Action<object, EventArgs> action)
        {
            subscribers.Add((subscriber, action));
        }

        public void Unsubscribe(object subscriber, Action<object, EventArgs> action)
        {
            subscribers.Remove((subscriber, action));
        }

        public void Notify(object notifier, EventArgs eventArgs)
        {
            if(notifier != owner)
            {
                // Can't raise 
                return;
            }

            foreach (var subscription in subscribers)
            {
                subscription.action.Invoke(notifier, eventArgs);
            }
        }

        public bool Get(object owner, string name)
        {
            return (owner == this.owner && name == this.name);
        }
    }
}
