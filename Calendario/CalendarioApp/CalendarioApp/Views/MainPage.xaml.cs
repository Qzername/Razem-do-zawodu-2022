using System;
using Xamarin.Forms;
using Xamarin.Plugin.Calendar.Models;

namespace CalendarioApp.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void CreateEventButtonClicked(object sender, EventArgs args)
        {
            DateTime selectedDate = Calendar.SelectedDate ?? DateTime.Now;
            await Navigation.PushAsync(new EventCreationPage(selectedDate), false);
        }
    }
}
