using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalendarioApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Views.MainPage());
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
