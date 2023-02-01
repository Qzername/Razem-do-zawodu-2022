using System;
using CalendarioApp.Model.Server;
using CalendarioApp.Managers;
using CalendarioApp.Model.App;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalendarioApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarPage : ContentPage
    {
        public CalendarPage()
        {
            InitializeComponent();
        }

        private async void CreateScheduleClicked(object sender, EventArgs args)
        {
            DateTime? selectedDate = Calendar.SelectedDate;
            if (selectedDate != null)
            {
                await Navigation.PushAsync(new ScheduleCreationPage(selectedDate));
            }
        }
    }
}
