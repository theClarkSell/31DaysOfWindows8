using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Day9_LiveTiles
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

        private void TextUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            XmlDocument tileData = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquareText04);
            XmlNodeList textData = tileData.GetElementsByTagName("text");
            textData[0].InnerText = "31 Days of Windows 8";
            TileNotification notification = new TileNotification(tileData);
            notification.ExpirationTime = DateTimeOffset.UtcNow.AddSeconds(30);

            TileUpdateManager.CreateTileUpdaterForApplication().Update(notification);
        }
    }
}
