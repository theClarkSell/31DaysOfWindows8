using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.ApplicationModel.Background;
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

namespace Day11_LockScreen
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
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            
            BackgroundAccessStatus status = await BackgroundExecutionManager.RequestAccessAsync();
        }

        private void LockScreenButton_Click(object sender, RoutedEventArgs e)
        {
            BackgroundAccessStatus status = BackgroundExecutionManager.GetAccessStatus();
            
            if ((status == BackgroundAccessStatus.AllowedWithAlwaysOnRealTimeConnectivity) ||
                (status == BackgroundAccessStatus.AllowedMayUseActiveRealTimeConnectivity))
            {
                XmlDocument badgeData = BadgeUpdateManager.GetTemplateContent(BadgeTemplateType.BadgeNumber);
                XmlNodeList badgeXML = badgeData.GetElementsByTagName("badge");
                ((XmlElement)badgeXML[0]).SetAttribute("value", "Playing");

                BadgeNotification badge = new BadgeNotification(badgeData);
                BadgeUpdateManager.CreateBadgeUpdaterForApplication().Update(badge);
            }
        }
    }
}
