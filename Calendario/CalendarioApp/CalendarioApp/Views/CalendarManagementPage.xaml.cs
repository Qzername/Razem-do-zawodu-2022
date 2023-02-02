using System;
using CalendarioApp.Model.App;
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
            Task task = (Task)((ListView)sender).SelectedItem;
            await Navigation.PushAsync(new TaskPage(task));
        }

        private void TaskTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        private async void CreateTaskClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TaskCreationPage());
        }

        private async void PrioritySelected(object sender, SelectedItemChangedEventArgs e)
        {
            Priority priority = (Priority)((ListView)sender).SelectedItem;
            await Navigation.PushAsync(new PriorityPage(priority));
        }

        private void PriorityTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        private async void CreatePriorityClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PriorityCreationPage());
        }
    }
}