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
            //First, we grab the specific template we want to use.
            XmlDocument tileData = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquareBlock);

            //Then we grab a reference to the node we want to update.
            XmlNodeList textData = tileData.GetElementsByTagName("text");

            //Then we set the value of that node.
            textData[0].InnerText = "31";
            textData[1].InnerText = "Days of Windows 8";

            //Then we create a TileNotification object with that data.
            TileNotification notification = new TileNotification(tileData);

            //We can optionally set an expiration date on the notification.
            notification.ExpirationTime = DateTimeOffset.UtcNow.AddSeconds(30);

            //Finally, we push the update to the tile.
            TileUpdateManager.CreateTileUpdaterForApplication().Update(notification);
        }

        private void LargeImageTileUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            //Create the Large Tile exactly the same way.
            XmlDocument largeTileData = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWidePeekImage01);
            XmlNodeList largeTextData = largeTileData.GetElementsByTagName("text");
            XmlNodeList imageData = largeTileData.GetElementsByTagName("image");
            largeTextData[0].InnerText = "Funny cat";
            largeTextData[1].InnerText = "This cat looks like it's trying to eat your face.";
            ((XmlElement)imageData[0]).SetAttribute("src", "ms-appx:///Assets/9-XAML-CatImage.png");
            //((XmlElement)imageData[0]).SetAttribute("src", "http://jeffblankenburg.com/downloads/9-XAML-CatImage.png");

            //Create a Small Tile notification also (not required, but recommended.)
            XmlDocument smallTileData = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquarePeekImageAndText02);
            XmlNodeList smallTileText = smallTileData.GetElementsByTagName("text");
            XmlNodeList smallTileImage = smallTileData.GetElementsByTagName("image");
            smallTileText[0].InnerText = "Funny cat";
            smallTileText[1].InnerText = "This cat looks like it's trying to eat your face.";
            ((XmlElement)smallTileImage[0]).SetAttribute("src", "ms-appx:///Assets/9-XAML-CatImageSmall.png");

            //Merge the two updates into one <visual> XML node
            IXmlNode newNode = largeTileData.ImportNode(smallTileData.GetElementsByTagName("binding").Item(0), true);
            largeTileData.GetElementsByTagName("visual").Item(0).AppendChild(newNode);

            //Create the notification the same way.
            TileNotification notification = new TileNotification(largeTileData);
            notification.ExpirationTime = DateTimeOffset.UtcNow.AddSeconds(30);

            //Push the update to the tile.
            TileUpdateManager.CreateTileUpdaterForApplication().Update(notification);

        }

        private void ClearNotificationsButton_Click(object sender, RoutedEventArgs e)
        {
            TileUpdateManager.CreateTileUpdaterForApplication().Clear();
        }
    }
}
