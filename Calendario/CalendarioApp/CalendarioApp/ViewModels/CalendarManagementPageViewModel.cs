using System.Collections.ObjectModel;
using System.Windows.Input;
using CalendarioApp.Managers;
using CalendarioApp.Model.Server;
using Xamarin.Forms;

namespace CalendarioApp.ViewModels
{
    public class CalendarManagementPageViewModel : BasePageViewModel
    {
        public CalendarManagementPageViewModel() : base()
        {
            Tasks = ServerManager.Tasks;
            Priorities = ServerManager.Priorities;
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
    }
}
