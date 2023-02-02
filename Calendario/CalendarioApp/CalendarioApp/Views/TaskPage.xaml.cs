using System;
using CalendarioApp.Managers;
using CalendarioApp.Model.Server;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalendarioApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskPage : ContentPage
    {
        private Task Task;

        public TaskPage(Task task)
        {
            InitializeComponent();
            Task = task;
            TaskName.Text = task.Name;
            TaskDescription.Text = task.Description;
        }

        private async void DeleteTaskClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new SyncPage());
            await ServerManager.RemoveTask(Task);
            await ServerManager.Sync();
            await Navigation.PopToRootAsync();
        }
    }
}