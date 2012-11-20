using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Day21_CameraCapture
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

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private async void Camera_Clicked(object sender, TappedRoutedEventArgs e)
        {
            TurnOffPanels();
            
            CameraCaptureUI camera = new CameraCaptureUI();
            camera.PhotoSettings.CroppedAspectRatio = new Size(16, 9);
            StorageFile photo = await camera.CaptureFileAsync(CameraCaptureUIMode.Photo);

            if (photo != null)
            {
                BitmapImage bmp = new BitmapImage();
                IRandomAccessStream stream = await photo.OpenAsync(FileAccessMode.Read);
                bmp.SetSource(stream);
                ImageSource.Source = bmp;
                ImageSource.Visibility = Visibility.Visible;
            }
        }

        private void TurnOffPanels()
        {
            ImageSource.Visibility = Visibility.Collapsed;
            VideoSource.Visibility = Visibility.Collapsed;
        }

        private async void Video_Clicked(object sender, TappedRoutedEventArgs e)
        {
            TurnOffPanels();

            CameraCaptureUI videocamera = new CameraCaptureUI();
            videocamera.VideoSettings.Format = CameraCaptureUIVideoFormat.Mp4;
            videocamera.VideoSettings.AllowTrimming = true;
            videocamera.VideoSettings.MaxDurationInSeconds = 30;
            videocamera.VideoSettings.MaxResolution = CameraCaptureUIMaxVideoResolution.HighestAvailable;

            StorageFile video = await videocamera.CaptureFileAsync(CameraCaptureUIMode.Video);

            if (video != null)
            {
                IRandomAccessStream stream = await video.OpenAsync(FileAccessMode.Read);
                VideoSource.SetSource(stream, "video/mp4");
                VideoSource.Visibility = Visibility.Visible;
            }
        }
    }
}
