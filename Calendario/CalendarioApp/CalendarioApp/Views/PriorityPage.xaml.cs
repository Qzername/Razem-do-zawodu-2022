using System;
using CalendarioApp.Managers;
using CalendarioApp.Model.App;
using CalendarioApp.Model.Server;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalendarioApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PriorityPage : ContentPage
    {
        private Priority Priority;

        public PriorityPage(Priority priority)
        {
            InitializeComponent();
            Priority = priority;
            PriorityName.Text = priority.Name;
        }

        private async void DeletePriorityClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new SyncPage());
            // await ServerManager.RemovePriority(Priority);
            ServerManager.ClearEvents();
            await ServerManager.Setup();
            await Navigation.PopToRootAsync();
        }
    }
}