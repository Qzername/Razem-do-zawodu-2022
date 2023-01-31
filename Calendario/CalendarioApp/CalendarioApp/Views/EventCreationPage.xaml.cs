using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CalendarioApp.Managers;
using CalendarioApp.Model.Server;

namespace CalendarioApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventCreationPage : ContentPage
    {
        public DateTime Date;

        public EventCreationPage(DateTime? date)
        {
            Date = date ?? DateTime.Now;
            InitializeComponent();
        }

        async void CreateEventButtonClicked(object sender, EventArgs args)
        {
            if (EventTitle == null)
            {
                await App.Current.MainPage.DisplayAlert("Błąd!", "Tytuł nie może być pusty.", "Ok");
                return;
            }

            await Navigation.PushAsync(new SyncPage());
            Date = Date + EventTimePicker.Time;
            TaskCreation task = new TaskCreation() { Name = EventTitle.Text, Description = EventDescription.Text };
            ScheduleCreation schedule = new ScheduleCreation() { DateBegin = Date.Ticks, TaskID = 1};
            await ServerManager.AddTaskAndSchedule(task, schedule);
            await ServerManager.GetTasks();
            await Navigation.PopToRootAsync();
        }
    }
}