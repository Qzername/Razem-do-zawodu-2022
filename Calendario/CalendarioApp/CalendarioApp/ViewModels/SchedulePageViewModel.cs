using System;

namespace CalendarioApp.ViewModels
{
    internal class SchedulePageViewModel : BasePageViewModel
    {
        public SchedulePageViewModel() : base()
        {
            TimeNow = DateTime.Now;
        }

        private DateTime _timeNow;

        public DateTime TimeNow
        {
            get => _timeNow;
            set => SetProperty(ref _timeNow, value);
        }
    }
}