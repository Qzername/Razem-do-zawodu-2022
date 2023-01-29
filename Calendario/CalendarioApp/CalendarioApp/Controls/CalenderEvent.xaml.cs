using CalendarioApp.Model;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalendarioApp.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalenderEvent : ContentView
    {
        public static BindableProperty CalenderEventCommandProperty =
            BindableProperty.Create(nameof(CalenderEventCommand), typeof(ICommand), typeof(CalenderEvent), null);

        public CalenderEvent()
        {
            InitializeComponent();
            Setup(Application.Current.RequestedTheme);
        }

        private void Setup(OSAppTheme theme)
        {
            if (theme == OSAppTheme.Dark) EventTitle.TextColor = Color.White;
            else EventTitle.TextColor = Color.Black;
        }

        public ICommand CalenderEventCommand
        {
            get => (ICommand)GetValue(CalenderEventCommandProperty);
            set => SetValue(CalenderEventCommandProperty, value);
        }

        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            if (BindingContext is AdvancedEventModel eventModel)
                CalenderEventCommand?.Execute(eventModel);
        }
    }
}
