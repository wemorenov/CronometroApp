using System;
using System.Windows;
using System.Windows.Threading;

namespace Cronometro
{

    public partial class MainWindow : Window
    {
        private readonly TimerServices _timerServices;

        public MainWindow(TimerServices timerServices)
        {
            InitializeComponent();
            _timerServices = timerServices;
            _timerServices.TimeElaspsed += RefreshTime;
            btnStop.IsEnabled = false;
            btnPause.IsEnabled = false;
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            _timerServices.Start();

            btnStart.IsEnabled = false;
            btnPause.IsEnabled = true;
            btnStop.IsEnabled = true;
            btnPause.Content = "Pause";
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            _timerServices.Stop();

            btnStart.IsEnabled = true;
            btnStop.IsEnabled = false;
            btnPause.IsEnabled = false;
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            if (_timerServices.IsExecuting)
            {
                _timerServices!.Pause();

                btnStart.IsEnabled = false;
                btnStop.IsEnabled = true;
                btnPause.IsEnabled = true;
                btnPause.Content = "Resume";

            }
            else
            {
                
                _timerServices!.Pause();

                btnPause.IsEnabled = false;
                btnPause.Content = "Pause";
            }
            

        }

        private void RefreshTime(TimeSpan time)
        {
            lblTime.Content = time.ToString(@"hh\:mm\:ss");
        }
    }
}
