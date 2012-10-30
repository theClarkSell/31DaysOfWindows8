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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Day4_NewControls
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

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AppBar));
        }

        private void FlipViewButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(FlipViewExample));
        }

        private void GridViewButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GridViewExample));
        }

        private void ProgressRingButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ProgressRingExample));
        }

        private void ScrollViewerButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ScrollViewerExample));
        }

    }
}
