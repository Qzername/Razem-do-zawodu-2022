using System;
using CalendarioApp.Managers;
using CalendarioApp.Model.Server;
using CalendarioApp.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalendarioApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : TabbedPage
    {
        public LoginPage ()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void LoginClicked(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PushAsync(new SyncPage());

            if (LoginRemember.IsChecked)
            {
                Preferences.Clear();
                SaveProperties(true);
            }

            try
            {
                await ServerManager.Login(new AccountCredentials { Login = LoginUsername.Text, Password = LoginPassword.Text });
                await ServerManager.Sync();
                ChangeNavigation();
            }

            catch (Exception ex)
            {
                Preferences.Clear();
                await App.Current.MainPage.DisplayAlert("Błąd!", $"Logowanie nie powiodło się. {ex}", "Ok");
            }

            await App.Current.MainPage.Navigation.PopToRootAsync();
        }

        private async void RegisterClicked(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PushAsync(new SyncPage());

            if (RegisterPassword2.Text != RegisterPassword1.Text)
            {
                await App.Current.MainPage.DisplayAlert("Błąd!", "Hasła nie zgadzają się.", "Ok");
                return;
            }

            if (RegisterRemember.IsChecked)
            {
                Preferences.Clear();
                SaveProperties(false);
            }

            try
            {
                await ServerManager.Register(new AccountCredentials { Login = RegisterUsername.Text, Password = RegisterPassword1.Text });
                await ServerManager.Sync();
                ChangeNavigation();
            }

            catch
            {
                Preferences.Clear();
                await App.Current.MainPage.DisplayAlert("Błąd!", "Rejestracja nie powiodła się.", "Ok");
            }

            await App.Current.MainPage.Navigation.PopToRootAsync();
        }

        private void RememberCheckedTapped(object sender, EventArgs e)
        {
            if (LoginRemember.IsChecked)
            {
                LoginRemember.IsChecked = false;
                RegisterRemember.IsChecked = false;
            }

            else
            {
                LoginRemember.IsChecked = true;
                RegisterRemember.IsChecked = true;
            }
        }

        private void LoginRememberCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (LoginRemember.IsChecked) RegisterRemember.IsChecked = true;
            else RegisterRemember.IsChecked = false;
        }

        private void RegisterRememberCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (RegisterRemember.IsChecked) LoginRemember.IsChecked = true;
            else LoginRemember.IsChecked = false;
        }

        private void ChangeNavigation()
        {
            App.Current.MainPage = new NavigationPage(new MainPage());
            App.Current.MainPage.SetBinding(VisualElement.BackgroundColorProperty, "PageBackground");
            App.Current.MainPage.BindingContext = new BasePageViewModel();
        }

        private async void SaveProperties(bool usingLogin)
        {
            if (usingLogin)
            {
                Preferences.Set("username", LoginUsername.Text);
                Preferences.Set("password", LoginPassword.Text);
            }

            else
            {
                Preferences.Set("username", RegisterUsername.Text);
                Preferences.Set("password", RegisterPassword1.Text);
            }
        }
    }
}