using System;
using CalendarioApp.Model.Server;
using CalendarioApp.Managers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalendarioApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
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
