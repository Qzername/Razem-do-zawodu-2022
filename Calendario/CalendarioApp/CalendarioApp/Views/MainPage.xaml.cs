using System;
using CalendarioApp.Model.Server;
using CalendarioApp.Managers;
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
                    await ServerManager.Login(new AccountCredentials
                    {
                        Login = "testlogin",
                        Password = "SeX123@a"
                    });
                }

                catch { await App.Current.MainPage.DisplayAlert("Błąd!", "Logowanie nie powiodło się.", "Ok"); }

                try
                {
                    await ServerManager.GetTasks();
                }

                catch (Exception e) { await App.Current.MainPage.DisplayAlert("Błąd!", "Pobranie listy wydarzeń nie powiodło się.", "Ok"); }

                await Navigation.PopToRootAsync();
            });

            InitializeComponent();
        }

        private async void CreateEventButtonClicked(object sender, EventArgs args)
        {
            DateTime? selectedDate = Calendar.SelectedDate;
            if (selectedDate != null)
            {
                await Navigation.PushAsync(new EventCreationPage(selectedDate));
            }
        }
    }
}
