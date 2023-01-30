using System;

namespace CalendarioApp.Model.App
{
    public class CalendarPickerResult
    {
        public bool IsSuccess { get; set; }

        public DateTime? SelectedDate { get; set; }
    }
}
