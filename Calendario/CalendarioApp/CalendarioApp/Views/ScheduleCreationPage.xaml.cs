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

            if (ScheduleEndDisabled.IsChecked) EndDate = StartDate;

            if (StartDate.Ticks <= EndDate.Ticks)
            {
                if (ReminderPicker.SelectedItem == null)
                {
                    await App.Current.MainPage.DisplayAlert("Błąd!", "Priorytet nie został wybrany.", "Ok");
                    return;
                }

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
                        reminder = (StartDate.AddDays(-1).Date + ReminderTimePicker.Time).Ticks;
                        break;
                    case 2: // 1 hour before
                        reminder = StartDate.AddHours(-1).Ticks;
                        break;
                    case 3: // 10 minutes before
                        reminder = StartDate.AddMinutes(-10).Ticks;
                        break;
                    case 4: // Custom date
                        reminder = (ReminderDatePicker.Date + ReminderTimePicker.Time).Ticks;
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
                await App.Current.MainPage.DisplayAlert("Błąd!", "Zakończenie wydarzenia nie może być szybciej niż rozpoczęcie.", "Ok");
            }
        }

        private void IsScheduleAllDayCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (IsScheduleAllDay.IsChecked)
            {
                ScheduleStart.IsVisible = false;
                ScheduleEndDisabledLayout.IsVisible = false;
                ScheduleEnd.IsVisible = false;
            }

            else
            {
                ScheduleStart.IsVisible = true;
                ScheduleEndDisabledLayout.IsVisible = true;
                ScheduleEnd.IsVisible = true;
            }

            ScheduleEndDisabled.IsChecked = false;
        }

        private void IsScheduleAllDayCheckedTapped(object sender, EventArgs e)
        {
            if (IsScheduleAllDay.IsChecked)
            {
                IsScheduleAllDay.IsChecked = false;
                ScheduleStart.IsVisible = true;
                ScheduleEndDisabledLayout.IsVisible = true;
                ScheduleEnd.IsVisible = true;
            }

            else
            {
                IsScheduleAllDay.IsChecked = true;
                ScheduleStart.IsVisible = false;
                ScheduleEndDisabledLayout.IsVisible = false;
                ScheduleEnd.IsVisible = false;
            }

            ScheduleEndDisabled.IsChecked = false;
        }

        private void ScheduleEndDisabledCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (ScheduleEndDisabled.IsChecked) ScheduleEnd.IsVisible = false;
            else ScheduleEnd.IsVisible = true;
        }

        private void ScheduleEndDisabledCheckedTapped(object sender, EventArgs e)
        {
            if (ScheduleEndDisabled.IsChecked)
            {
                ScheduleEndDisabled.IsChecked = false;
                ScheduleEnd.IsVisible = true;
            }

            else
            {
                ScheduleEndDisabled.IsChecked = true;
                ScheduleEnd.IsVisible = false;
            }
        }

        private void ReminderPickerSelectedChanged(object sender, EventArgs e)
        {

            switch (ReminderPicker.SelectedIndex)
            {
                case 1:
                    ReminderTimePickerLayout.IsVisible = true;
                    break;
                case 4:
                    ReminderDatePickerLayout.IsVisible = true;
                    ReminderTimePickerLayout.IsVisible = true;
                    break;
                default:
                    ReminderDatePickerLayout.IsVisible = false;
                    ReminderTimePickerLayout.IsVisible = false;
                    break;
            }
        }
    }
}