using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CalendarioApp.Managers;
using CalendarioApp.Model.Server;

namespace CalendarioApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScheduleCreationPage : ContentPage
    {
        private DateTime StartDate;
        private DateTime EndDate;

        public ScheduleCreationPage(DateTime? date)
        {
            StartDate = EndDate = date ?? DateTime.Now;
            InitializeComponent();
        }

        async void CreateScheduleClicked(object sender, EventArgs args)
        {
            if (IsScheduleAllDay.IsChecked)
            {
                StartDate = StartDate.Date;
                EndDate = EndDate.Date.AddDays(1).AddTicks(-1);
            }

            else
            {
                StartDate = StartDate.Date + ScheduleStartTimePicker.Time;
                EndDate = EndDate.Date + ScheduleEndTimePicker.Time;
            }

            if (StartDate.Ticks <= EndDate.Ticks)
            {
                await Navigation.PushAsync(new SyncPage());

                var selectedTask = (Task)TaskPicker.SelectedItem;
                ScheduleCreation schedule = new ScheduleCreation() { DateBegin = StartDate.Ticks, DateEnd = EndDate.Ticks, TaskID = selectedTask.ID };

                try
                {
                    await ServerManager.AddSchedule(schedule);
                }

                catch
                {
                    await App.Current.MainPage.DisplayAlert("Błąd!", "Zaplanowanie wydarzenia nie powiodło się.", "Ok");
                }

                ServerManager.ClearEvents();
                await ServerManager.Setup();

                await Navigation.PopToRootAsync();
                await Navigation.PushAsync(new CalendarPage());
            }

            else
            {
                EndDate = StartDate;
                ScheduleEndTimePicker.Time = ScheduleStartTimePicker.Time;
            }
        }

        private void IsScheduleAllDayTapped(object sender, EventArgs e)
        {
            if (IsScheduleAllDay.IsChecked == true)
            {
                IsScheduleAllDay.IsChecked = false;
                ScheduleStart.IsVisible = true;
                ScheduleEnd.IsVisible = true;
            }

            else
            {
                IsScheduleAllDay.IsChecked = true;
                ScheduleStart.IsVisible = false;
                ScheduleEnd.IsVisible = false;
            }
        }
    }
}