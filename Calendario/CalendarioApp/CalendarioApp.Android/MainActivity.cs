using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Plugin.LocalNotification;
using Xamarin.Essentials;
using Xamarin.Forms;
using Android.Views;

namespace CalendarioApp.Droid
{
    [Activity(Label = "Calendario", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Platform.Init(this, savedInstanceState);
            Forms.Init(this, savedInstanceState);
            LocalNotificationCenter.CreateNotificationChannel();
            LoadApplication(new App());
            LocalNotificationCenter.NotifyNotificationTapped(Intent);

            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            Window.ClearFlags(WindowManagerFlags.TranslucentStatus);
            StatusBarVisibility flag = (StatusBarVisibility)SystemUiFlags.LightStatusBar;

            if (Xamarin.Forms.Application.Current.RequestedTheme == OSAppTheme.Dark)
            {
                Window.SetStatusBarColor(Android.Graphics.Color.Argb(255, 0, 0, 0));
                Window.DecorView.SystemUiVisibility = false ? flag : 0;
            }

            else
            {
                Window.SetStatusBarColor(Android.Graphics.Color.Argb(255, 255, 255, 255));
                Window.DecorView.SystemUiVisibility = true ? flag : 0;

            }


            Xamarin.Forms.Application.Current.RequestedThemeChanged += (s, a) =>
            {
                Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
                Window.ClearFlags(WindowManagerFlags.TranslucentStatus);
                StatusBarVisibility flag = (StatusBarVisibility)SystemUiFlags.LightStatusBar;

                OSAppTheme theme = a.RequestedTheme;
                
                if (theme == OSAppTheme.Dark)
                {
                    Window.SetStatusBarColor(Android.Graphics.Color.Argb(255, 0, 0, 0));
                    Window.DecorView.SystemUiVisibility = false ? flag : 0;
                }

                else
                {
                    Window.SetStatusBarColor(Android.Graphics.Color.Argb(255, 255, 255, 255));
                    Window.DecorView.SystemUiVisibility = true ? flag : 0;
                }
            };
        }

        protected override void OnNewIntent(Intent intent)
	    {
		    LocalNotificationCenter.NotifyNotificationTapped(intent);
		    base.OnNewIntent(intent);
	    }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}