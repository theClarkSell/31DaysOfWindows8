using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Devices.Sensors;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Day27_Inclinometer
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        Inclinometer inclinometer;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            inclinometer = Inclinometer.GetDefault();
            if (inclinometer != null)
            {
                inclinometer.ReadingChanged += inclinometer_ReadingChanged;
                Data.Visibility = Visibility.Visible;
            }
            else
            {
                NoSensorMessage.Visibility = Visibility.Visible;
            }
        }

        private async void inclinometer_ReadingChanged(Inclinometer sender, InclinometerReadingChangedEventArgs args)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                PitchValue.Text = args.Reading.PitchDegrees.ToString();
                RollValue.Text = args.Reading.RollDegrees.ToString();
                YawValue.Text = args.Reading.YawDegrees.ToString();
                TimeStamp.Text = args.Reading.Timestamp.ToString();
            });
        }
    }
}
