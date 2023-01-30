using System;
using CalendarioApp.Model.Server;
using Xamarin.Forms;

namespace CalendarioApp.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Navigation.PushAsync(new SyncPage(), false);

                try
                {
                    await ServerManager.ServerManager.Login(new AccountCredentials
                    {
                        Login = "testlogin",
                        Password = "SeX123@a"
                    });
                }

                catch { await App.Current.MainPage.DisplayAlert("Błąd!", "Logowanie nie powiodło się...", "Ok"); }

                try
                {
                    string test = await ServerManager.ServerManager.GetTasks();
                    await App.Current.MainPage.DisplayAlert("Debug", test, "Ok");
                }

                catch (Exception e) { await App.Current.MainPage.DisplayAlert("Błąd!", $"Pobranie listy wydarzeń nie powiodło się...\n{e.Message}", "Ok"); }

                await Navigation.PopToRootAsync(false);
            });

            InitializeComponent();
        }

        async void CreateEventButtonClicked(object sender, EventArgs args)
        {
            DateTime selectedDate = Calendar.SelectedDate ?? DateTime.Now;
            await Navigation.PushAsync(new EventCreationPage(selectedDate), false);
        }
    }
}
