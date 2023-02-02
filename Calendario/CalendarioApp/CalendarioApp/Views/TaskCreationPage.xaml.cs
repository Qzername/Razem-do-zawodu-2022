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

            if (string.IsNullOrWhiteSpace(TaskDescription.Text))
            {
                await App.Current.MainPage.DisplayAlert("Błąd!", "Opis zadania nie może być pusty.", "Ok");
                return;
            }

            await Navigation.PushAsync(new SyncPage());

            var selectedPriority = (Priority)PriorityPicker.SelectedItem;
            TaskCreation task = new TaskCreation() { Name = TaskName.Text, Description = TaskDescription.Text, PriorityID = selectedPriority.ID };

            try
            {
                await ServerManager.AddTask(task);
            }

            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Błąd!", $"Stworzenie zadania nie powiodło się.{ex.Message}", "Ok");
            }

            await ServerManager.Sync();

            await Navigation.PopToRootAsync();
        }
    }
}