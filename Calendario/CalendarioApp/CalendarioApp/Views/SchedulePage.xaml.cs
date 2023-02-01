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
            NavigationPage.SetHasNavigationBar(this, false);
            ScheduleTitle.Text = schedule.Name;
            ScheduleDatePicker.Date = schedule.Starting;
            ScheduleTimePicker.Time = schedule.Starting.TimeOfDay;
        }

        private void DeleteScheduleClicked(object sender, EventArgs args)
        {
        }
    }
}