using System;
using Windows.Devices.Sensors;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Day26_Gyrometer
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        Gyrometer gyrometer;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            gyrometer = Gyrometer.GetDefault();
            if (gyrometer != null)
            {
                gyrometer.ReadingChanged += gyrometer_ReadingChanged;
                Data.Visibility = Visibility.Visible;
            }
            else
            {
                NoSensorMessage.Visibility = Visibility.Visible;
            }
        }

        async void gyrometer_ReadingChanged(Gyrometer sender, GyrometerReadingChangedEventArgs args)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                XValue.Text = args.Reading.AngularVelocityX.ToString();
                YValue.Text = args.Reading.AngularVelocityY.ToString();
                ZValue.Text = args.Reading.AngularVelocityZ.ToString();
                TimeStamp.Text = args.Reading.Timestamp.ToString();

                xLine.X2 = xLine.X1 + args.Reading.AngularVelocityX * 200;
                yLine.Y2 = yLine.Y1 - args.Reading.AngularVelocityY * 200;
                zLine.X2 = zLine.X1 - args.Reading.AngularVelocityZ * 100;
                zLine.Y2 = zLine.Y1 + args.Reading.AngularVelocityZ * 100;
            });
        }
    }
}
