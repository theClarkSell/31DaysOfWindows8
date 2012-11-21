using System;
using Windows.Devices.Sensors;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Day24_LightSensor
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        LightSensor sensor;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            sensor = LightSensor.GetDefault();
            if (sensor != null)
            {
                sensor.ReadingChanged += sensor_ReadingChanged;
                Data.Visibility = Visibility.Visible;
            }
            else
            {
                NoSensorMessage.Visibility = Visibility.Visible;
            }
        }

        async void sensor_ReadingChanged(LightSensor sender, LightSensorReadingChangedEventArgs args)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                Lux.Text = args.Reading.IlluminanceInLux.ToString();
                TimeStamp.Text = args.Reading.Timestamp.ToString();
            });
        }
    }
}
