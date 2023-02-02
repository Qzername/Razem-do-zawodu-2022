using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CalendarioApp.Managers;
using CalendarioApp.Model.Server;

namespace CalendarioApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskCreationPage : ContentPage
    {
        public TaskCreationPage()
        {
            InitializeComponent();
        }

        async void CreateTaskClicked(object sender, EventArgs args)
        {
            if (string.IsNullOrWhiteSpace(TaskName.Text))
            {
                await App.Current.MainPage.DisplayAlert("Błąd!", "Nazwa zadania nie może być pusta.", "Ok");
                return;
            }

            await Navigation.PushAsync(new SyncPage());

            TaskCreation task = new TaskCreation() { Name = TaskName.Text, Description = TaskDescription.Text};

            try
            {
                await ServerManager.AddTask(task);
            }

            catch
            {
                await App.Current.MainPage.DisplayAlert("Błąd!", "Stworzenie zadania nie powiodło się.", "Ok");
            }

            ServerManager.ClearEvents();
            await ServerManager.Setup();

            await Navigation.PopToRootAsync();
            await Navigation.PushAsync(new CalendarPage());
        }
    }
}