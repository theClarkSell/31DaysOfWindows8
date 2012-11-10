using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Devices.Geolocation;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Day14_Geolocation
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        Geolocator location;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            location = new Geolocator();
            location.PositionChanged += location_PositionChanged;
            location.StatusChanged += location_StatusChanged;
            //GetLocationDataOnce();
        }

        async private void GetLocationDataOnce()
        {
            Geoposition position = await location.GetGeopositionAsync().AsTask();

            LatitudeValue.Text = position.Coordinate.Latitude.ToString();
            LongitudeValue.Text = position.Coordinate.Longitude.ToString();
            AccuracyValue.Text = position.Coordinate.Accuracy.ToString();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            location.PositionChanged -= location_PositionChanged;
            location.StatusChanged -= location_StatusChanged;
        }

        async void location_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                Geoposition position = args.Position;

                LatitudeValue.Text = position.Coordinate.Latitude.ToString();
                LongitudeValue.Text = position.Coordinate.Longitude.ToString();
                AccuracyValue.Text = position.Coordinate.Accuracy.ToString();
                
                TimestampValue.Text = position.Coordinate.Timestamp.ToString();

                if (position.Coordinate.Altitude != null)
                    AltitudeValue.Text = position.Coordinate.Altitude.ToString()
                                         + "(+- " + position.Coordinate.AltitudeAccuracy.ToString() + ")";
                if (position.Coordinate.Heading != null)
                    HeadingValue.Text = position.Coordinate.Heading.ToString();
                if (position.Coordinate.Speed != null)
                    SpeedValue.Text = position.Coordinate.Speed.ToString();
            });
        }

        async void location_StatusChanged(Geolocator sender, StatusChangedEventArgs args)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                StatusValue.Text = args.Status.ToString();
            });
        }
    }
}
