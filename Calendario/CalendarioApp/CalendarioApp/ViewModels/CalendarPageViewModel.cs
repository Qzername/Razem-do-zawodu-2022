using CalendarioApp.Managers;
using CalendarioApp.Model.App;
using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Input;
using CalendarioApp.Views;
using Xamarin.Forms;
using Xamarin.Plugin.Calendar.Enums;
using Xamarin.Plugin.Calendar.Models;

namespace CalendarioApp.ViewModels
{
    public class CalendarPageViewModel : BasePageViewModel
    {
        public ICommand DayTappedCommand => new Command<DateTime>((date) => DayTapped(date));
        public ICommand SwipeLeftCommand => new Command(() => ChangeShownUnit(1));
        public ICommand SwipeRightCommand => new Command(() => ChangeShownUnit(-1));
        public ICommand SwipeUpCommand => new Command(() => { ShownDate = DateTime.Today; });

        public ICommand EventSelectedCommand => new Command(async (item) => await ExecuteEventSelectedCommand(item));

        public CalendarPageViewModel() : base()
        {
            Culture = CultureInfo.CreateSpecificCulture("pl-PL");
            SelectedDate = null;
            ShownDate = DateTime.Today;
        }

        public EventCollection Events
        {
            get => ServerManager.Events;
        }

        private DateTime _shownDate = DateTime.Today;

        public DateTime ShownDate
        {
            get => _shownDate;
            set => SetProperty(ref _shownDate, value);
        }

        private WeekLayout _calendarLayout = WeekLayout.Month;

        public WeekLayout CalendarLayout
        {
            get => _calendarLayout;
            set => SetProperty(ref _calendarLayout, value);
        }

        private DateTime? _selectedDate = DateTime.Today;

        public DateTime? SelectedDate
        {
            get => _selectedDate;
            set => SetProperty(ref _selectedDate, value);
        }

        private CultureInfo _culture = CultureInfo.InvariantCulture;

        public CultureInfo Culture
        {
            get => _culture;
            set => SetProperty(ref _culture, value);
        }

        private static void DayTapped(DateTime date)
        {
            // var message = $"Received tap event from date: {date}";
            // await App.Current.MainPage.DisplayAlert("DayTapped", message, "Ok");
        }

        private async Task ExecuteEventSelectedCommand(object item)
        {
            if (item is AdvancedEventModel eventModel)
            {
                await App.Current.MainPage.Navigation.PushAsync(new SchedulePage(eventModel));
            }
        }

        private void ChangeShownUnit(int amountToAdd)
        {
            switch (CalendarLayout)
            {
                case WeekLayout.Week:
                case WeekLayout.TwoWeek:
                    ChangeShownWeek(amountToAdd);
                    break;

                case WeekLayout.Month:
                default:
                    ChangeShownMonth(amountToAdd);
                    break;
            }
        }

        private void ChangeShownMonth(int monthsToAdd)
        {
            ShownDate.AddMonths(monthsToAdd);
        }

        private void ChangeShownWeek(int weeksToAdd)
        {
            ShownDate.AddDays(weeksToAdd * 7);
        }
    }
}
