using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalendarioApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventCreationPage : ContentPage
    {
        public DateTime Date;

        public EventCreationPage(DateTime date)
        {
            Date = date;
            InitializeComponent();
        }

        async void CreateEventButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new SyncPage(), false);
            Date = Date + EventTimePicker.Time;
            await App.Current.MainPage.DisplayAlert($"Created an event: {EventTitle.Text}", $"Ticks: {Date.Ticks.ToString()}", "Ok");
            await Navigation.PopToRootAsync(false);
        }
    }
}