using System;
using System.Diagnostics;
using Android.Text.Format;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalendarioApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClockPage : TabbedPage
    {
        private bool stopTimer;
        private bool stopStopWatch;
        private Stopwatch stopWatch = new Stopwatch();

        public ClockPage()
        {
            InitializeComponent();
        }

        private void TimerStartClicked(object sender, EventArgs e)
        {
            if (Timer.Time.TotalSeconds == 0) return;

            stopTimer = false;
            Timer.IsEnabled = false;
            TimerStart.IsVisible = false;
            TimerStop.IsVisible = true;

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                if (stopTimer) return false;

                Device.BeginInvokeOnMainThread(() =>
                {
                    Timer.Time -= TimeSpan.FromSeconds(1);
                });

                return true;
            });
        }

        private void TimerStopClicked(object sender, EventArgs e)
        {
            stopWatch.Stop();
            stopTimer = true;
            Timer.IsEnabled = true;
            TimerStart.IsVisible = true;
            TimerStop.IsVisible = false;
        }

        private void StopWatchStartClicked(object sender, EventArgs e)
        {
            stopStopWatch = false;
            StopWatchStart.IsVisible = false;
            StopWatchStop.IsVisible = true;
            stopWatch.Start();

            Device.StartTimer(TimeSpan.FromMilliseconds(1), () =>
            {
                if (stopStopWatch) return false;

                Device.BeginInvokeOnMainThread(() =>
                {
                    StopWatch.Time = stopWatch.Elapsed;
                });

                return true;
            });
        }

        private void StopWatchStopClicked(object sender, EventArgs e)
        {
            stopStopWatch = true;
            StopWatchStart.IsVisible = true;
            StopWatchStop.IsVisible = false;
        }
    }
}