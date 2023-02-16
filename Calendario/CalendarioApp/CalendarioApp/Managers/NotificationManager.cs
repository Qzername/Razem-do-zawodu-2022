using System;
using System.Threading.Tasks;
using Plugin.LocalNotification;

namespace CalendarioApp.Managers
{
    class NotificationManager
    {
        private static int notificationCount;

        public static async Task Schedule(string title, string description, DateTime scheduleBegin, DateTime scheduleEnd, DateTime? scheduleRemind)
        {
            await LocalNotificationCenter.Current.RequestNotificationPermission();

            string notificationDescription;

            if (scheduleBegin.TimeOfDay.ToString() == "00:00:00" && scheduleEnd.TimeOfDay.ToString() == "23:59:59.9999999") notificationDescription = $"Cały dzień\n{description}";
            else if (scheduleBegin.Ticks == scheduleEnd.Ticks) notificationDescription = $"{scheduleBegin.ToShortTimeString()}\n{description}";
            else notificationDescription = $"{scheduleBegin.ToShortTimeString()} - {scheduleEnd.ToShortTimeString()}\n{description}";

            if (scheduleRemind != null) await LocalNotificationCenter.Current.Show(CreateReminder(title, notificationDescription, scheduleBegin, scheduleRemind));
            await LocalNotificationCenter.Current.Show(CreateNotification(title, notificationDescription, scheduleBegin));
        }

        public static void CancelAll()
        {
            LocalNotificationCenter.Current.CancelAll();
        }

        private static NotificationRequest CreateNotification(string title, string description, DateTime time)
        {
            notificationCount++;

            return new NotificationRequest
            {
                NotificationId = notificationCount,
                Title = title,
                Description = description,
                ReturningData = "Notification tapped.",
                Schedule = { NotifyTime = time }
            };
        }

        private static NotificationRequest CreateReminder(string title, string description, DateTime time, DateTime? timeRemind)
        {
            notificationCount++;

            return new NotificationRequest
            {
                NotificationId = notificationCount,
                Title = $"Nadchodzące wydarzenie ({time.ToShortDateString()}): {title}",
                Description = description,
                ReturningData = "Notification tapped.",
                Schedule = { NotifyTime = timeRemind }
            };
        }
    }
}
