using System;
using System.Threading.Tasks;
using Plugin.LocalNotification;

namespace CalendarioApp.Managers
{
    class NotificationManager
    {
        private static int notificationCount = 0;

        public static async Task Schedule(string title, string description, DateTime scheduleBegin, DateTime scheduleEnd)
        {
            await LocalNotificationCenter.Current.RequestNotificationPermission();

            notificationCount++;

            var notificationDayBefore = new NotificationRequest
            {
                NotificationId = notificationCount,
                Title = $"Nadchodzące wydarzenie (jutro): {title}",
                Description = $"{scheduleBegin.Hour}:{scheduleBegin.Minute} - {description}",
                ReturningData = "Notification tapped.", // Returning data when tapped on notification.
                Schedule =
                {
                    NotifyTime = scheduleBegin.AddDays(-1) // Used for Scheduling local notification, if not specified notification will show immediately.
                }
            };

            await LocalNotificationCenter.Current.Show(notificationDayBefore);

            notificationCount++;

            var notification = new NotificationRequest
            {
                NotificationId = notificationCount,
                Title = title,
                Description = $"{scheduleBegin.Hour}:{scheduleBegin.Minute} - {description}",
                ReturningData = "Notification tapped.", // Returning data when tapped on notification.
                Schedule =
                {
                    NotifyTime = scheduleBegin // Used for Scheduling local notification, if not specified notification will show immediately.
                }
            };

            await LocalNotificationCenter.Current.Show(notification);
        }

        public static void CancelAll()
        {
            LocalNotificationCenter.Current.CancelAll();
        }
    }
}
