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

        public void Setup(OSAppTheme theme)
        {
            Calendar.ShownDate = DateTime.Now;

            if (theme == OSAppTheme.Dark)
            {
                Calendar.ArrowsColor = Color.DimGray;
                Calendar.YearLabelColor = Color.White;
                Calendar.MonthLabelColor = Color.White;
                Calendar.DaysTitleColor = Color.White;
                Calendar.DeselectedDayTextColor = Color.White;
                Calendar.OtherMonthDayColor = Color.DimGray;
            }

            else if (theme == OSAppTheme.Light)
            {
                Calendar.ArrowsColor = Color.Black;
                Calendar.YearLabelColor = Color.Black;
                Calendar.MonthLabelColor = Color.Black;
                Calendar.DaysTitleColor = Color.Black;
                Calendar.DeselectedDayTextColor = Color.Black;
                Calendar.OtherMonthDayColor = Color.Gray;
            }
        }
    }
}
