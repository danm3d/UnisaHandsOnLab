using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace App6
{
    public sealed partial class MainPage : Page
    {
        //private instance fields
        private DispatcherTimer timer = new DispatcherTimer();
        private TimeSpan firstBreakStartTime;
        private TimeSpan secondBreakStartTime;
        private TimeSpan homeTime;
        private TimeSpan now;

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            firstBreakStartTime = FirstBreakStart.Time;
            secondBreakStartTime = SecondBreakStart.Time;
            homeTime = HomeTime.Time;
            now = DateTime.Now.TimeOfDay;

            //Timer setup
            timer.Interval = new TimeSpan(0, 0, 0, 1);
            timer.Tick += Each_Tick;
        }

        private void Each_Tick(object sender, object e)
        {
            //First break
            var firstBreakStartTime = FirstBreakStart.Time;
            var firstBreakEndTime = firstBreakStartTime + TimeSpan.FromSeconds(10);

            //Second break
            var secondBreakStartTime = SecondBreakStart.Time;
            var secondBreakEndTime = secondBreakStartTime + TimeSpan.FromSeconds(10);

            //Home time
            var homeTime = HomeTime.Time;

            //Time right now
            var now = DateTime.Now.TimeOfDay;

            var timeDifference = TimeSpan.Zero;

            if (now < firstBreakStartTime)
            {
                timeDifference = firstBreakStartTime - now;
                Label.Text = "First break in...";
                TimeTrimmer(timeDifference);
            }
            if (now > firstBreakStartTime && now < firstBreakEndTime)
            {
                Label.Text = "Yay!";
                TimeTextBlock.Text = "It is now first break!!";
            }
            if (now > firstBreakEndTime && now < secondBreakStartTime)
            {
                timeDifference = secondBreakStartTime - now;
                Label.Text = "Second break in...";
                TimeTrimmer(timeDifference);
            }
            if (now > secondBreakStartTime && now < secondBreakEndTime)
            {
                Label.Text = "Yay!";
                TimeTextBlock.Text = "It is now second break!!";
            }
            if (now > secondBreakEndTime && now < homeTime)
            {
                timeDifference = homeTime - now;
                Label.Text = "Home time in...";
                TimeTrimmer(timeDifference);
            }
            if (now > homeTime)
            {
                Label.Text = "HEY!";
                TimeTextBlock.Text = "Shouldn't you be home already??";
            }
        }

        public void TimeTrimmer(TimeSpan timeDifference)
        {
            if (timeDifference < TimeSpan.FromMinutes(1))
            {
                TimeTextBlock.Text = timeDifference.Seconds.ToString() + "s";
            }
            if (timeDifference < TimeSpan.FromHours(1) && timeDifference > TimeSpan.FromMinutes(1))
            {
                TimeTextBlock.Text = timeDifference.Minutes.ToString() + "m" + timeDifference.Seconds.ToString() + "s";                
            }
            if (timeDifference < TimeSpan.FromDays(1) && timeDifference > TimeSpan.FromHours(1))
            {
                TimeTextBlock.Text = timeDifference.Hours.ToString() + "h" + timeDifference.Minutes.ToString() + "m" + timeDifference.Seconds.ToString() + "s";                
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        private void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }
    }
}