using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace CalendarioApp.ViewModels
{
    public class BasePageViewModel : INotifyPropertyChanged
    {
        public BasePageViewModel()
        {
            OSAppTheme theme = Application.Current.RequestedTheme;

            if (theme == OSAppTheme.Dark)
            {
                PageBackground = Color.Black;
                FrameBackground = Color.FromRgb(15, 15, 15);
                PrimaryColor = Color.White;
                SecondaryColor = Color.Black;
                DisabledColor = Color.DimGray;
            }

            else
            {
                PageBackground = Color.White;
                FrameBackground = Color.FromRgb(255, 255, 255);
                PrimaryColor = Color.Black;
                SecondaryColor = Color.White;
                DisabledColor = Color.Gray;
            }
        }
        
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void SetProperty<TData>(ref TData storage, TData value, [CallerMemberName] string propertyName = "")
        {
            if (storage?.Equals(value) == true)
                return;

            storage = value;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        private Color _pageBackground;

        public Color PageBackground
        {
            get => _pageBackground;
            set => SetProperty(ref _pageBackground, value);
        }

        private Color _frameBackground;

        public Color FrameBackground
        {
            get => _frameBackground;
            set => SetProperty(ref _frameBackground, value);
        }

        private Color _primaryColor;

        public Color PrimaryColor
        {
            get => _primaryColor;
            set => SetProperty(ref _primaryColor, value);
        }

        private Color _secondaryColor;

        public Color SecondaryColor
        {
            get => _secondaryColor;
            set => SetProperty(ref _secondaryColor, value);
        }

        private Color _disabledColor;

        public Color DisabledColor
        {
            get => _disabledColor;
            set => SetProperty(ref _disabledColor, value);
        }
    }
}
