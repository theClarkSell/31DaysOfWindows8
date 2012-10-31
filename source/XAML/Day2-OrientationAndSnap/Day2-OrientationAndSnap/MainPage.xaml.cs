using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Sensors;

namespace Day2_OrientationAndSnap
{
    public sealed partial class MainPage : Page
    {
        private SimpleOrientationSensor orientationSensor;
        
        public MainPage()
        {
            this.InitializeComponent();
            orientationSensor = SimpleOrientationSensor.GetDefault();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (orientationSensor != null)
                orientationSensor.OrientationChanged += new TypedEventHandler<SimpleOrientationSensor, SimpleOrientationSensorOrientationChangedEventArgs>(orientationSensor_OrientationChanged);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            if (orientationSensor != null)
                orientationSensor.OrientationChanged -= orientationSensor_OrientationChanged;
            base.OnNavigatingFrom(e);
        }

        async private void orientationSensor_OrientationChanged(SimpleOrientationSensor sender, SimpleOrientationSensorOrientationChangedEventArgs args)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                ShowOrientationText(args.Orientation);
            });
        }

        private void ShowOrientationText(SimpleOrientation simpleOrientation)
        {
            switch (simpleOrientation)
            {
                case SimpleOrientation.NotRotated:
                    AlertBox.Text = "Not Rotated";
                    break;
                case SimpleOrientation.Rotated90DegreesCounterclockwise:
                    AlertBox.Text = "90 Degrees CounterClockwise";
                    break;
                case SimpleOrientation.Rotated180DegreesCounterclockwise:
                    AlertBox.Text = "180 Degrees Rotated";
                    break;
                case SimpleOrientation.Rotated270DegreesCounterclockwise:
                    AlertBox.Text = "270 Degrees Rotated CounterClockwise";
                    break;
                case SimpleOrientation.Facedown:
                    AlertBox.Text = "Face Down";
                    break;
                case SimpleOrientation.Faceup:
                    AlertBox.Text = "Face Up";
                    break;
                default:
                    AlertBox.Text = "Unknown";
                    break;
            }
        }
    }
}
