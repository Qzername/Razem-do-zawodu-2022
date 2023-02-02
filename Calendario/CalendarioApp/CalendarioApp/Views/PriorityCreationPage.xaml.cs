using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CalendarioApp.Managers;
using CalendarioApp.Model.Server;
using static Android.App.ActivityManager;

namespace CalendarioApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PriorityCreationPage : ContentPage
    {
        public PriorityCreationPage()
        {
            InitializeComponent();
        }

        async void CreatePriorityClicked(object sender, EventArgs args)
        {
            if (string.IsNullOrWhiteSpace(PriorityName.Text))
            {
                await App.Current.MainPage.DisplayAlert("Błąd!", "Nazwa priorytetu nie może być pusta.", "Ok");
                return;
            }

            await Navigation.PushAsync(new SyncPage());

            PriorityCreation priority = new PriorityCreation() { Name = PriorityName.Text, ColorHex = "#ff0000" };

            try
            {
                await ServerManager.AddPriority(priority);
            }

            catch
            {
                await App.Current.MainPage.DisplayAlert("Błąd!", "Stworzenie priorytetu nie powiodło się.", "Ok");
            }

            ServerManager.ClearEvents();
            await ServerManager.Setup();

            await Navigation.PopToRootAsync();
            await Navigation.PushAsync(new CalendarPage());
        }
    }
}