using System;
using Xamarin.Forms;
using Xamarin.Plugin.Calendar.Models;

namespace CalendarioApp
{
    public partial class MainPage : ContentPage
    {
        public EventCollection Events { get; set; }

        public MainPage()
        {
            InitializeComponent();
            Setup(Application.Current.RequestedTheme);
        }

        private void Setup(OSAppTheme theme)
        {
            Calendar.ShownDate = DateTime.Now;

            if (theme == OSAppTheme.Dark)
            {
                CalendarioContentPage.BackgroundColor = Color.Black;

                CalendarFrame.BackgroundColor = Color.FromRgb(15, 15, 15);

                Calendar.ArrowsColor = Color.White;
                Calendar.YearLabelColor = Color.White;
                Calendar.MonthLabelColor = Color.White;
                Calendar.DaysTitleColor = Color.White;
                Calendar.DeselectedDayTextColor = Color.White;
                Calendar.OtherMonthDayColor = Color.DimGray;
                Calendar.SelectedDateColor = Color.White;
                Calendar.SelectedDayTextColor = Color.Black;
                Calendar.SelectedDayBackgroundColor = Color.White;
                Calendar.SelectedTodayTextColor = Color.Black;
                Calendar.EventIndicatorSelectedTextColor = Color.Black;
                Calendar.EventIndicatorTextColor = Color.White;
            }

            else if (theme == OSAppTheme.Light)
            {
                CalendarioContentPage.BackgroundColor = Color.White;

                CalendarFrame.BackgroundColor = Color.FromRgb(245, 245, 245);

                Calendar.ArrowsColor = Color.Black;
                Calendar.YearLabelColor = Color.Black;
                Calendar.MonthLabelColor = Color.Black;
                Calendar.DaysTitleColor = Color.Black;
                Calendar.DeselectedDayTextColor = Color.Black;
                Calendar.OtherMonthDayColor = Color.Gray;
                Calendar.SelectedDateColor = Color.Black;
                Calendar.SelectedDayTextColor = Color.White;
                Calendar.SelectedDayBackgroundColor = Color.Black;
                Calendar.SelectedTodayTextColor = Color.White;
                Calendar.EventIndicatorSelectedTextColor = Color.White;
                Calendar.EventIndicatorTextColor = Color.Black;
            }
        }
    }
}
