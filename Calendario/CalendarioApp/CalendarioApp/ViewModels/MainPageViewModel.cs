using CalendarioApp.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Plugin.Calendar.Enums;
using Xamarin.Plugin.Calendar.Models;

namespace CalendarioApp.ViewModels
{
    public class MainPageViewModel : BasePageViewModel
    {
        public ICommand DayTappedCommand => new Command<DateTime>(async (date) => await DayTapped(date));
        public ICommand SwipeLeftCommand => new Command(() => ChangeShownUnit(1));
        public ICommand SwipeRightCommand => new Command(() => ChangeShownUnit(-1));
        public ICommand SwipeUpCommand => new Command(() => { ShownDate = DateTime.Today; });

        public ICommand EventSelectedCommand => new Command(async (item) => await ExecuteEventSelectedCommand(item));

        public MainPageViewModel() : base()
        {
            OSAppTheme theme = Application.Current.RequestedTheme;

            if (theme == OSAppTheme.Dark)
            {
                IndicatorSelectedColor = Color.White;
                PageBackground = Color.Black;
                FrameBackground = Color.FromRgb(15, 15, 15);
                PrimaryColor = Color.White;
                SecondaryColor = Color.Black;
                DisabledColor = Color.DimGray;
            }

            else
            {
                IndicatorSelectedColor = Color.Black;
                PageBackground = Color.White;
                FrameBackground = Color.FromRgb(240, 240, 240);
                PrimaryColor = Color.Black;
                SecondaryColor = Color.White;
                DisabledColor = Color.Gray;
            }

            Culture = CultureInfo.CreateSpecificCulture("pl-PL");

            Events = new EventCollection
            {
                // [DateTime.Now.AddDays(-3)] = new List<AdvancedEventModel>(GenerateEvents(10, "Cool")),
                [DateTime.Now.AddDays(-6)] = new DayEventCollection<AdvancedEventModel>(Color.Purple, IndicatorSelectedColor)
                {
                    new AdvancedEventModel { Name = "Pobudka...", Description = "Nowy dzień, nowy ja :)", Starting= new DateTime().AddHours(06).AddMinutes(30) },
                    new AdvancedEventModel { Name = "Koniec szkoły :D", Description = "Wkońcu!!!", Starting= new DateTime().AddHours(14).AddMinutes(45) }
                }
            };

            ShownDate = DateTime.Today;
            SelectedDate = DateTime.Today;
        }

        public EventCollection Events { get; }

        private Color IndicatorSelectedColor;

        private Color _pageBackground;

        public Color PageBackground
        {
            get => _pageBackground;
            set => SetProperty(ref _pageBackground, value);
        }

        private Color _frameBackground;

        public Color FrameBackground
        {
            get => _frameBackground;
            set => SetProperty(ref _frameBackground, value);
        }

        private Color _primaryColor;

        public Color PrimaryColor
        {
            get => _primaryColor;
            set => SetProperty(ref _primaryColor, value);
        }

        private Color _secondaryColor;

        public Color SecondaryColor
        {
            get => _secondaryColor;
            set => SetProperty(ref _secondaryColor, value);
        }

        private Color _disabledColor;

        public Color DisabledColor
        {
            get => _disabledColor;
            set => SetProperty(ref _disabledColor, value);
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

        private static async Task DayTapped(DateTime date)
        {
            // var message = $"Received tap event from date: {date}";
            // await App.Current.MainPage.DisplayAlert("DayTapped", message, "Ok");
        }

        private async Task ExecuteEventSelectedCommand(object item)
        {
            if (item is AdvancedEventModel eventModel)
            {
                var title = $"Selected: {eventModel.Name}";
                var message = $"Starts: {eventModel.Starting:HH:mm}{Environment.NewLine}Details: {eventModel.Description}";
                await App.Current.MainPage.DisplayAlert(title, message, "Ok");
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
