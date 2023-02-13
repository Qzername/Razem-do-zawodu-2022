using System;
using CalendarioApp.Managers;
using CalendarioApp.Model.Server;
using CalendarioApp.ViewModels;
using CalendarioApp.Views;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace CalendarioApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            try
            {
                if (!Preferences.ContainsKey("username") || !Preferences.ContainsKey("password")) throw new Exception("Username or password is not cached");

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await ServerManager.Login(new AccountCredentials { Login = Preferences.Get("username", null), Password = Preferences.Get("password", null) });
                    await ServerManager.Sync();
                });

                ServerManager.UserName = Preferences.Get("username", null);

                MainPage = new NavigationPage(new MainPage());
            }

            catch
            {
                MainPage = new NavigationPage(new LoginPage());
            }

            MainPage.SetBinding(VisualElement.BackgroundColorProperty, "PageBackground");
            MainPage.BindingContext = new BasePageViewModel();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
