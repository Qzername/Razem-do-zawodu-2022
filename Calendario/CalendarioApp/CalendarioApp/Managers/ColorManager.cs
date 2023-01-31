﻿using System.Threading.Tasks;
using Xamarin.Forms;

namespace CalendarioApp.Managers
{
    public class ColorManager
    {
        public static OSAppTheme Theme = Application.Current.RequestedTheme;

        public static Color GetPrimaryColor()
        {
            if (Theme == OSAppTheme.Dark) { return Color.White; }
            else { return Color.Black; }
        }
    }
}
