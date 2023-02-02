using System;
using CalendarioApp.Managers;
using CalendarioApp.Model.App;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalendarioApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SchedulePage : ContentPage
    {
        AdvancedEventModel Schedule;

        public SchedulePage(AdvancedEventModel schedule)
        {
            InitializeComponent();
            Schedule = schedule;
            ScheduleTitle.Text = schedule.Name;
            ScheduleDatePicker.Date = schedule.Starting;
            ScheduleTimePicker.Time = schedule.Starting.TimeOfDay;
        }

        private async void DeleteScheduleClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new SyncPage());
            await ServerManager.RemoveSchedule(Schedule);
            ServerManager.ClearEvents();
            await ServerManager.Setup();
            await Navigation.PopToRootAsync();
        }
    }
}