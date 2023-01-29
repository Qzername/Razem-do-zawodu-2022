using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalendarioApp.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarFooter : ContentView
    {
        public CalendarFooter()
        {
            InitializeComponent();
            Setup(Application.Current.RequestedTheme);
        }

        private void Setup(OSAppTheme theme)
        {
            if (theme == OSAppTheme.Dark) showHideLabel.TextColor = Color.White;
            else showHideLabel.TextColor = Color.Black;
        }
    }
}
