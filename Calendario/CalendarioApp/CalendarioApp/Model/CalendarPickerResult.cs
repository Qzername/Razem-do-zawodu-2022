using System;

namespace CalendarioApp.Model
{
    public class CalendarPickerResult
    {
        public bool IsSuccess { get; set; }

        public DateTime? SelectedDate { get; set; }
    }
}
