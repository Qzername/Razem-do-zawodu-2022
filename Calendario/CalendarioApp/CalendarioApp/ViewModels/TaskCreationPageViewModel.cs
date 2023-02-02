using System;
using System.Collections.ObjectModel;
using CalendarioApp.Managers;
using CalendarioApp.Model.Server;

namespace CalendarioApp.ViewModels
{
    internal class TaskCreationPageViewModel : BasePageViewModel
    {
        public TaskCreationPageViewModel() : base()
        {
            Priorities = ServerManager.Priorities;
            TimeNow = DateTime.Now;
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