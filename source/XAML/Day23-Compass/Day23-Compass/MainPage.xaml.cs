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

namespace Day23_Compass
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        Compass c;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            c = Compass.GetDefault();
            if (c != null)
            {
                c.ReadingChanged += c_ReadingChanged;
                Data.Visibility = Visibility.Visible;
            }
            else NoSensorMessage.Visibility = Visibility.Visible;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            c.ReadingChanged -= c_ReadingChanged;
        }

        async void c_ReadingChanged(Compass sender, CompassReadingChangedEventArgs args)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                MagneticNorth.Text = args.Reading.HeadingMagneticNorth.ToString();
                if (args.Reading.HeadingTrueNorth != null)
                {
                    TrueNorth.Text = args.Reading.HeadingTrueNorth.ToString();
                }
                TimeStamp.Text = args.Reading.Timestamp.ToString();
            });
        }
    }
}
