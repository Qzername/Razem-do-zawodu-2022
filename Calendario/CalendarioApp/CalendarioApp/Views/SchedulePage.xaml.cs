using CalendarioApp.Managers;
using CalendarioApp.Model.Server;
using System;
using CalendarioApp.Model.App;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalendarioApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SchedulePage : ContentPage
    {
        public SchedulePage(AdvancedEventModel schedule)
        {
            InitializeComponent();
            ScheduleTitle.Text = schedule.Name;
            ScheduleDatePicker.Date = schedule.Starting;
            ScheduleTimePicker.Time = schedule.Starting.TimeOfDay;
        }

        async void DeleteScheduleClicked(object sender, EventArgs args)
        {
        }
    }
}