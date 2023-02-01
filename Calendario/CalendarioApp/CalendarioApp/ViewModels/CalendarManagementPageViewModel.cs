using System;
using System.Collections.Generic;
using System.Linq;
using CalendarioApp.Managers;
using CalendarioApp.Model.Server;
using Xamarin.Forms;

namespace CalendarioApp.ViewModels
{
    public class CalendarManagementPageViewModel : BasePageViewModel
    {
        public CalendarManagementPageViewModel() : base()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                Task[] tasks = await ServerManager.GetTasks();
                Tasks = tasks.ToList();

                Schedule[] schedules = await ServerManager.GetSchedulesUsingTasks(tasks);
                Schedules = schedules.ToList();

                Priority[] priorities = await ServerManager.GetPriorities();
                Priorities = priorities.ToList();
            });
        }

        private List<Task> _tasks;

        public List<Task> Tasks
        {
            get => _tasks;
            set => SetProperty(ref _tasks, value);
        }

        private List<Schedule> _schedules;

        public List<Schedule> Schedules
        {
            get => _schedules;
            set => SetProperty(ref _schedules, value);
        }

        private List<Priority> _priorities;

        public List<Priority> Priorities
        {
            get => _priorities;
            set => SetProperty(ref _priorities, value);
        }
    }
}
