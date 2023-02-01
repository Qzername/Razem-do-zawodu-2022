using CalendarioApp.Managers;
using CalendarioApp.Model.Server;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalendarioApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabPage : TabbedPage
    {
        public TabPage()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Navigation.PushAsync(new SyncPage());

                try
                {
                    await ServerManager.Login(new AccountCredentials
                    {
                        Login = "testlogin",
                        Password = "SeX123@a"
                    });

                    try
                    {
                        ServerManager.ClearEvents();
                        await ServerManager.Setup();
                    }

                    catch { await App.Current.MainPage.DisplayAlert("Błąd!", "Pobranie listy wydarzeń nie powiodło się.", "Ok"); }
                }

                catch { await App.Current.MainPage.DisplayAlert("Błąd!", "Logowanie nie powiodło się.", "Ok"); }

                await Navigation.PopToRootAsync();
            });

            InitializeComponent();
        }
    }
}