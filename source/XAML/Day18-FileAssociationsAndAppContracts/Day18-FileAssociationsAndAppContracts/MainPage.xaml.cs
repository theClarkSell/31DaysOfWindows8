using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Day18_FileAssociationsAndAppContracts
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

        private async void OpenExcelButton_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("ms-appx:///Assets/PeriodicTable.xls");
            StorageFile sf = await StorageFile.GetFileFromApplicationUriAsync(uri);
            await Launcher.LaunchFileAsync(sf);
        }

        private async void OpenExcelButtonWithWarning_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("ms-appx:///Assets/PeriodicTable.xls");
            StorageFile sf = await StorageFile.GetFileFromApplicationUriAsync(uri);

            LauncherOptions options = new LauncherOptions();
            options.TreatAsUntrusted = true;

            bool action = await Launcher.LaunchFileAsync(sf, options);
        }

        private async void OpenExcelButtonWithOpenWithMenu_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("ms-appx:///Assets/PeriodicTable.xls");
            StorageFile sf = await StorageFile.GetFileFromApplicationUriAsync(uri);

            LauncherOptions options = new LauncherOptions();
            options.DisplayApplicationPicker = true;
            options.UI.InvocationPoint = GetPosition(sender as FrameworkElement);
            options.UI.PreferredPlacement = Placement.Below;

            bool action = await Launcher.LaunchFileAsync(sf, options);
        }

        private Point GetPosition(FrameworkElement sender)
        {
            GeneralTransform transform = sender.TransformToVisual(null);
            Point location = transform.TransformPoint(new Point());
            location.Y = location.Y + sender.ActualHeight;

            return location;
        }
    }
}
