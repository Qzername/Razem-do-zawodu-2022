using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CalendarioApp.Managers;
using CalendarioApp.Model.App;
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
            ReminderPicker.SelectedItem = ReminderPicker.ItemsSource[0];
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
                var selectedReminder = (Reminder)ReminderPicker.SelectedItem;
                long? reminder;

                switch (selectedReminder.ID)
                {
                    case 0: // Never
                        reminder = null;
                        break;
                    case 1: // 1 day before
                        reminder = StartDate.AddDays(-1).Ticks;
                        break;
                    case 2: // 1 hour before
                        reminder = StartDate.AddHours(-1).Ticks;
                        break;
                    case 3: // 10 minutes before
                        reminder = StartDate.AddMinutes(-10).Ticks;
                        break;
                    default: // Never
                        reminder = null;
                        break;
                }

                ScheduleCreation schedule = new ScheduleCreation()
                    { DateBegin = StartDate.Ticks, DateEnd = EndDate.Ticks, DateRemind = reminder, TaskID = selectedTask.ID };

                try
                {
                    await ServerManager.AddSchedule(schedule);
                }

                catch
                {
                    await App.Current.MainPage.DisplayAlert("Błąd!", "Zaplanowanie wydarzenia nie powiodło się.", "Ok");
                }

                await ServerManager.Sync();

                await Navigation.PopToRootAsync();
            }

            else
            {
                EndDate = StartDate;
                ScheduleEndTimePicker.Time = ScheduleStartTimePicker.Time;
            }
        }

        private void IsScheduleAllDayCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (IsScheduleAllDay.IsChecked)
            {
                ScheduleStart.IsVisible = false;
                ScheduleEnd.IsVisible = false;
            }

            else
            {
                ScheduleStart.IsVisible = true;
                ScheduleEnd.IsVisible = true;
            }
        }

        private void IsScheduleAllDayCheckedTapped(object sender, EventArgs e)
        {
            if (IsScheduleAllDay.IsChecked)
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