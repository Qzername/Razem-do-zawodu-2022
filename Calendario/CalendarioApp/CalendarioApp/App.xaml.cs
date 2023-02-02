using CalendarioApp.Managers;
using CalendarioApp.Model.Server;
using CalendarioApp.ViewModels;
using CalendarioApp.Views;
using Xamarin.Forms;

namespace CalendarioApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new TabPage());
            MainPage.SetBinding(VisualElement.BackgroundColorProperty, "PageBackground");
            MainPage.BindingContext = new BasePageViewModel();

            Device.BeginInvokeOnMainThread(async () =>
            {
                await App.Current.MainPage.Navigation.PushAsync(new SyncPage());

                await ServerManager.Login(new AccountCredentials
                {
                    Login = "testlogin",
                    Password = "SeX123@a"
                });

                await ServerManager.Sync();

                await App.Current.MainPage.Navigation.PopToRootAsync();
            });
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
