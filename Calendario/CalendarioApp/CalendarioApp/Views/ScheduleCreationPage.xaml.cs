using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CalendarioApp.Managers;
using CalendarioApp.Model.Server;

namespace CalendarioApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScheduleCreationPage : ContentPage
    {
        public DateTime Date;

        public ScheduleCreationPage(DateTime? date)
        {
            Date = date ?? DateTime.Now;
            Device.BeginInvokeOnMainThread(async () =>
            {
                var Tasks = await ServerManager.GetTasks();
                TaskPicker.ItemsSource = Tasks;
                TaskPicker.ItemDisplayBinding = new Binding("Name");
            });
            InitializeComponent();
        }

        async void CreateScheduleClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new SyncPage());

            Date = Date + ScheduleTimePicker.Time;
            var selectedTask = (Task)TaskPicker.SelectedItem;
            ScheduleCreation schedule = new ScheduleCreation() { DateBegin = Date.Ticks, TaskID = selectedTask.ID};

            try
            {
                await ServerManager.AddSchedule(schedule);
            }

            catch { await App.Current.MainPage.DisplayAlert("Błąd!", "Zaplanowanie wydarzenia nie powiodło się.", "Ok"); }

            await ServerManager.ClearEvents();
            await ServerManager.Setup();
            await Navigation.PopToRootAsync();
        }
    }
}