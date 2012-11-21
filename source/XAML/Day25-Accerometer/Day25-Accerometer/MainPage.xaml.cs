using System;
using Windows.Devices.Sensors;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Day25_Accerometer
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        Accelerometer accelerometer;
        int shakes;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            accelerometer = Accelerometer.GetDefault();
            if (accelerometer != null)
            {
                accelerometer.ReadingChanged += accelerometer_ReadingChanged;
                accelerometer.Shaken += accelerometer_Shaken;
                Data.Visibility = Visibility.Visible;
            }
            else
            {
                NoSensorMessage.Visibility = Visibility.Visible;
            }
        }

        async void accelerometer_ReadingChanged(Accelerometer sender, AccelerometerReadingChangedEventArgs args)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                XValue.Text = args.Reading.AccelerationX.ToString();
                YValue.Text = args.Reading.AccelerationY.ToString();
                ZValue.Text = args.Reading.AccelerationZ.ToString();
                TimeStamp.Text = args.Reading.Timestamp.ToString();
            });
        }

        async void accelerometer_Shaken(Accelerometer sender, AccelerometerShakenEventArgs args)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                shakes++;
                ShakeCount.Text = shakes.ToString();
            });
        }
    }
}
