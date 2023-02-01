﻿using CalendarioApp.Model.App;
using CalendarioApp.Views;
using System;
using System.Windows.Input;
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
            Setup(Application.Current.RequestedTheme);
        }

        private void Setup(OSAppTheme theme)
        {
            if (theme == OSAppTheme.Dark) TaskTitle.TextColor = Color.White;
            else TaskTitle.TextColor = Color.Black;
        }

        public ICommand CalenderEventCommand
        {
            get => (ICommand)GetValue(CalenderEventCommandProperty);
            set => SetValue(CalenderEventCommandProperty, value);
        }

        private async void TaskClicked(object sender, EventArgs e)
        {
            if (BindingContext is AdvancedEventModel eventModel)
            {
                CalenderEventCommand?.Execute(eventModel);
            }
        }
    }
}
