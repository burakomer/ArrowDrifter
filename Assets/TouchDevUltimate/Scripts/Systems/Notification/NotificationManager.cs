using System;
using System.Collections.Generic;
using UnityEngine;

namespace TouchDevUltimate.Systems.Notification
{
    public static class NotificationManager
    {
        public static List<Notification> notifications;

        /// <summary>
        /// Creates a notification channel.
        /// </summary>
        /// <param name="owner">Owner of the channel.</param>
        /// <param name="notificationName">Name of the channel.</param>
        public static void Create(object owner, string notificationName)
        {
            notifications.Add(new Notification(owner, notificationName));
        }

        /// <summary>
        /// Delete an existing notification channel.
        /// </summary>
        /// <param name="owner">Owner of the channel.</param>
        /// <param name="notificationName">Name of the channel.</param>
        public static void Delete(object owner, string notificationName)
        {
            int index = notifications.FindIndex(notification => notification.Get(owner, notificationName));
            notifications.RemoveAt(index);
        }

        /// <summary>
        /// Notify all subscribers by executing their actions.
        /// </summary>
        /// <param name="notificationName">Name of the channel.</param>
        /// <param name="notifier">Caller of the method.</param>
        /// <param name="eventArgs">The EventArgs object which holds the arguments.</param>
        public static void Notify(string notificationName, object notifier, EventArgs eventArgs)
        {
            notifications
                .Find(notification => notification.name == notificationName)
                .Notify(notifier, eventArgs);
        }

        /// <summary>
        /// Subscribe to an existing notification channel.
        /// </summary>
        /// <param name="notificationName">Name of the channel.</param>
        /// <param name="subscriber">Object of the subscriber.</param>
        /// <param name="action">The action to execute when notified.</param>
        public static void Subscribe(string notificationName, object subscriber, Action<object, EventArgs> action)
        {
            notifications
                .Find(notification => notification.name == notificationName)
                .Subscribe(subscriber, action);
        }

        /// <summary>
        /// Unsubscribe from an existing notification channel.
        /// </summary>
        /// <param name="notificationName">Name of the channel.</param>
        /// <param name="subscriber">Object of the subscriber.</param>
        /// <param name="action">The action that was given in subscription.</param>
        public static void Unsubscribe(string notificationName, object subscriber, Action<object, EventArgs> action)
        {
            notifications
                .Find(notification => notification.name == notificationName)
                .Unsubscribe(subscriber, action);
        }
    }
}
