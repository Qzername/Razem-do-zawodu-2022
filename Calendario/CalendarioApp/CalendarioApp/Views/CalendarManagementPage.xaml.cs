using System;
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

        private void TaskClicked(object sender, EventArgs e)
        {
            
        }

        private void ScheduleClicked(object sender, EventArgs e)
        {

        }

        private void PriorityClicked(object sender, EventArgs e)
        {

        }
    }
}