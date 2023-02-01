using System.Threading.Tasks;
using Plugin.LocalNotification;

namespace CalendarioApp.Managers
{
    class NotificationManager
    {
        public static async Task Test()
        {
            await LocalNotificationCenter.Current.RequestNotificationPermission();

            var notification = new NotificationRequest
            {
                NotificationId = 100,
                Title = "Test",
                Description = "Test Description",
                ReturningData = "Dummy data", // Returning data when tapped on notification.
            };

            await LocalNotificationCenter.Current.Show(notification);
        }
    }
}
