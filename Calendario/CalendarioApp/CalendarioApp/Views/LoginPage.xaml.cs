using System;
using System.Text.RegularExpressions;
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
        private static Regex loginRegex = new Regex(".{8,20}");
        private static Regex passwordRegex = new Regex("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,50}");

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

            catch
            {
                Preferences.Clear();
                await App.Current.MainPage.DisplayAlert("Błąd!", "Logowanie nie powiodło się.", "Ok");
            }

            await App.Current.MainPage.Navigation.PopToRootAsync();
        }

        private async void RegisterClicked(object sender, EventArgs e)
        {
            if (RegisterUsername.Text == null || RegisterPassword1.Text == null || !loginRegex.IsMatch(RegisterUsername.Text) || !passwordRegex.IsMatch(RegisterPassword1.Text))
            {
                await App.Current.MainPage.DisplayAlert("Błąd!", 
@"Nazwa konta lub hasło nie spełniają warunków:
- Nazwa konta jest większa od 8 i krótsza od 20 symboli
- Hasło nie zawiera spacji etc.
- Hasło jest większe od 8 i mniejsze od 50 symboli
- Hasło zawiera przynajmniej jeden znak specjalny (np. @!&)
- Hasło zawiera przynajmniej jedną cyfrę
- Hasło ma przynajmniej jeden znak duży
- Hasło ma przynajmniej jeden znak mały", 
                "Ok");
                return;
            }

            if (RegisterPassword2.Text != RegisterPassword1.Text)
            {
                await App.Current.MainPage.DisplayAlert("Błąd!", "Hasła nie zgadzają się.", "Ok");
                return;
            }

            await App.Current.MainPage.Navigation.PushAsync(new SyncPage());

            if (RegisterRemember.IsChecked)
            {
                Preferences.Clear();
                SaveProperties(false);
            }

            try
            {
                await ServerManager.Register(new AccountCredentials { Login = RegisterUsername.Text, Password = RegisterPassword1.Text });
                await ServerManager.AddPriority(new PriorityCreation() { Name = "Najważniejsze", ColorHex = "#FF0000" });
                await ServerManager.AddPriority(new PriorityCreation() { Name = "Ważniejsze", ColorHex = "#FFFF00" });
                await ServerManager.AddPriority(new PriorityCreation() { Name = "Ważne", ColorHex = "#00FF00" });
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

        private void LoginPasswordShowCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (LoginPasswordShow.IsChecked) LoginPassword.IsPassword = false;
            else LoginPassword.IsPassword = true;
        }

        private void LoginRememberCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (LoginRemember.IsChecked) RegisterRemember.IsChecked = true;
            else RegisterRemember.IsChecked = false;
        }

        private void RegisterPassword1ShowCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (RegisterPassword1Show.IsChecked) RegisterPassword1.IsPassword = false;
            else RegisterPassword1.IsPassword = true;
        }

        private void RegisterPassword2ShowCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (RegisterPassword2Show.IsChecked) RegisterPassword2.IsPassword = false;
            else RegisterPassword2.IsPassword = true;
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

        private void SaveProperties(bool usingLogin)
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