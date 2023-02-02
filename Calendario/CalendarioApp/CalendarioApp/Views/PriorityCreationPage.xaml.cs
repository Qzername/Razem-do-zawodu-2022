using System;
using CalendarioApp.Managers;
using CalendarioApp.Model.Server;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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

            await ServerManager.Sync();

            await Navigation.PopToRootAsync();
        }
    }
}