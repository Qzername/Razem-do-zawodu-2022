using CalendarioApp.Model.App;
using System;
using System.Globalization;
using System.Windows.Input;
using CalendarioApp.Model.Server;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalendarioApp.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalenderEvent : ContentView
    {
        public static BindableProperty CalenderEventCommandProperty = BindableProperty.Create(nameof(CalenderEventCommand), typeof(ICommand), typeof(CalenderEvent), null);

        public CalenderEvent()
        {
            InitializeComponent();
        }

        public ICommand CalenderEventCommand
        {
            get => (ICommand)GetValue(CalenderEventCommandProperty);
            set => SetValue(CalenderEventCommandProperty, value);
        }

        private void TaskClicked(object sender, EventArgs e)
        {
            if (BindingContext is AdvancedEventModel eventModel)
            {
                CalenderEventCommand?.Execute(eventModel);
            }
        }
    }
}
