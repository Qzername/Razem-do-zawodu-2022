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
        private AdvancedEventModel Schedule;

        public SchedulePage(AdvancedEventModel schedule)
        {
            InitializeComponent();
            Schedule = schedule;
            Task.Text = schedule.Name;
            ScheduleDatePicker.Date = schedule.Starting;
            ScheduleStartTimePicker.Time = schedule.Starting.TimeOfDay;
            DateTime ending = schedule.Ending ?? schedule.Starting;
            ScheduleEndTimePicker.Time = ending.TimeOfDay;
        }

        private async void DeleteScheduleClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new SyncPage());
            await ServerManager.RemoveSchedule(Schedule);
            await ServerManager.Sync();
            await Navigation.PopToRootAsync();
        }
    }
}