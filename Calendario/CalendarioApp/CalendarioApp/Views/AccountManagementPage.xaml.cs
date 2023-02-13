using CalendarioApp.ViewModels;
using System;
using CalendarioApp.Managers;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalendarioApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountManagementPage : ContentPage
    {
        public AccountManagementPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void LogoutClicked(object sender, EventArgs e)
        {
            Preferences.Clear();
            App.Current.MainPage = new NavigationPage(new LoginPage());
            App.Current.MainPage.SetBinding(VisualElement.BackgroundColorProperty, "PageBackground");
            App.Current.MainPage.BindingContext = new BasePageViewModel();
        }
    }
}