using CalendarioApp.Managers;

namespace CalendarioApp.ViewModels
{
    public class AccountManagementPageViewModel : BasePageViewModel
    {
        public string UserName
        {
            get => ServerManager.UserName;
        }
    }
}