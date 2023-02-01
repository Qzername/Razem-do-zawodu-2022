using System;
using CalendarioApp.Model.Server;
using CalendarioApp.Managers;
using CalendarioApp.Model.App;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalendarioApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Navigation.PushAsync(new SyncPage(), false);

                try
                {
                    await ServerManager.Login(new AccountCredentials
                    {
                        Login = "testlogin",
                        Password = "SeX123@a"
                    });
                }

                catch { await App.Current.MainPage.DisplayAlert("Błąd!", "Logowanie nie powiodło się.", "Ok"); }

                try
                {
                    ServerManager.ClearEvents();
                    await ServerManager.Setup();
                }

                catch { await App.Current.MainPage.DisplayAlert("Błąd!", "Pobranie listy wydarzeń nie powiodło się.", "Ok"); }

                await Navigation.PopToRootAsync();
            }); 

            InitializeComponent();
        }

        private async void NavigateCalendarClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new CalendarPage());
        }

        private async void NavigateAccountManagementClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new AccountManagementPage());
        }

        private async void NavigateCalendarManagementClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new CalendarManagementPage());
        }
    }
}
