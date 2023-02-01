using CalendarioApp.Views;
using Xamarin.Forms;

namespace CalendarioApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new TabPage());

            if (Application.Current.RequestedTheme == OSAppTheme.Dark) MainPage.BackgroundColor = Color.Black;
            else MainPage.BackgroundColor = Color.White;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
