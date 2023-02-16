using System;
using CalendarioApp.Model.Server;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalendarioApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarManagementPage : ContentPage
    {
        public CalendarManagementPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void TaskSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (((ListView)sender).SelectedItem == null) return;

            Task task = (Task)((ListView)sender).SelectedItem;
            await Navigation.PushAsync(new TaskPage(task));

            ((ListView)sender).SelectedItem = null;
        }

        private async void CreateTaskClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TaskCreationPage());
        }

        private async void PrioritySelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (((ListView)sender).SelectedItem == null) return;

            Priority priority = (Priority)((ListView)sender).SelectedItem;
            await Navigation.PushAsync(new PriorityPage(priority));

            ((ListView)sender).SelectedItem = null;
        }

        private async void CreatePriorityClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PriorityCreationPage());
        }
    }
}