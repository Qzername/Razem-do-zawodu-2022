using System;
using System.Collections.Generic;
using System.Linq;
using CalendarioApp.Managers;
using CalendarioApp.Model.Server;
using Xamarin.Forms;

namespace CalendarioApp.ViewModels
{
    internal class ScheduleCreationPageViewModel : BasePageViewModel
    {
        public ScheduleCreationPageViewModel() : base()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                Tasks = await ServerManager.GetTasks();
            });

            TimeNow = DateTime.Now;
        }

        private List<Task> _tasks;

        public List<Task> Tasks
        {
            get => _tasks;
            set => SetProperty(ref _tasks, value);
        }

        private DateTime _timeNow;

        public DateTime TimeNow
        {
            get => _timeNow;
            set => SetProperty(ref _timeNow, value);
        }
    }
}