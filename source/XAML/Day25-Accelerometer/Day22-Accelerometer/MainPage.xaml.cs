using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Devices.Sensors;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Day22_Accelerometer
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

        Accelerometer accelerometer;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            accelerometer = Accelerometer.GetDefault();

            if (accelerometer != null)
            {
                accelerometer.ReadingChanged += accelerometer_ReadingChanged;
                accelerometer.Shaken += accelerometer_Shaken;
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            accelerometer.ReadingChanged -= accelerometer_ReadingChanged;
            accelerometer.Shaken -= accelerometer_Shaken;
        }

        void accelerometer_Shaken(Accelerometer sender, AccelerometerShakenEventArgs args)
        {
            
        }

        void accelerometer_ReadingChanged(Accelerometer sender, AccelerometerReadingChangedEventArgs args)
        {
            
        }
    }
}
