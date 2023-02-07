using System;
using System.Collections.ObjectModel;
using CalendarioApp.Managers;
using CalendarioApp.Model.Server;

namespace CalendarioApp.ViewModels
{
    internal class ScheduleCreationPageViewModel : BasePageViewModel
    {
        public ScheduleCreationPageViewModel() : base()
        {
            Tasks = ServerManager.Tasks;
            Priorities = ServerManager.Priorities;
            TimeNow = DateTime.Now;
        }

        private ObservableCollection<Task> _tasks;

        public ObservableCollection<Task> Tasks
        {
            get => _tasks;
            set => SetProperty(ref _tasks, value);
        }

        private ObservableCollection<Priority> _priorities;

        public ObservableCollection<Priority> Priorities
        {
            get => _priorities;
            set => SetProperty(ref _priorities, value);
        }

        private DateTime _timeNow;

        public DateTime TimeNow
        {
            get => _timeNow;
            set => SetProperty(ref _timeNow, value);
        }
    }
}