using System;
using System.Collections.ObjectModel;
using CalendarioApp.Managers;
using CalendarioApp.Model.App;
using CalendarioApp.Model.Server;
using Java.Util.Jar;

namespace CalendarioApp.ViewModels
{
    public class ScheduleCreationPageViewModel : BasePageViewModel
    {
        public ScheduleCreationPageViewModel() : base()
        {
            Tasks = ServerManager.Tasks;
            Priorities = ServerManager.Priorities;
            Reminders = new ObservableCollection<Reminder>
            {
                new Reminder { Name = "Nigdy", ID = 0 },
                new Reminder { Name = "1 dzień przed", ID = 1 },
                new Reminder { Name = "1 godzinę przed", ID = 2 },
                new Reminder { Name = "10 minut przed", ID = 3 },
                new Reminder { Name = "Spersonalizuj datę", ID = 4 }
            };
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

        private ObservableCollection<Reminder> _reminders;

        public ObservableCollection<Reminder> Reminders
        {
            get => _reminders;
            set => SetProperty(ref _reminders, value);
        }
    }
}