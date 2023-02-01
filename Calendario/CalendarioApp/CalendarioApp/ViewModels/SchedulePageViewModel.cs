using System;
using System.Runtime.CompilerServices;
using CalendarioApp.Managers;
using CalendarioApp.Model.Server;
using CalendarioApp.Views;
using Xamarin.Forms;

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