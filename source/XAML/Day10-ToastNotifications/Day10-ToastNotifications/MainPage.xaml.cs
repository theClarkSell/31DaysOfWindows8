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

namespace Day10_ToastNotifications
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

        private void ToastNotification_Click(object sender, RoutedEventArgs e)
        {
            ToastTemplateType toastType = ToastTemplateType.ToastImageAndText02;
            XmlDocument toastXML = ToastNotificationManager.GetTemplateContent(toastType);
            XmlNodeList toastText = toastXML.GetElementsByTagName("text");
            XmlNodeList toastImages = toastXML.GetElementsByTagName("image");
            toastText[0].InnerText = "Funny cat";
            toastText[1].InnerText = "This cat looks like it's trying to eat your face.";
            ((XmlElement)toastImages[0]).SetAttribute("src", "ms-appx:///Assets/10-XAML-CatImageSmall.png");
            ((XmlElement)toastImages[0]).SetAttribute("alt", "Scary Cat Face");

            ToastNotification toast = new ToastNotification(toastXML);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }

        private void ToastNotificationOptions_Click(object sender, RoutedEventArgs e)
        {
            ToastTemplateType toastType = ToastTemplateType.ToastImageAndText02;
            XmlDocument toastXML = ToastNotificationManager.GetTemplateContent(toastType);
            XmlNodeList toastText = toastXML.GetElementsByTagName("text");
            XmlNodeList toastImages = toastXML.GetElementsByTagName("image");
            toastText[0].InnerText = "Funny cat";
            toastText[1].InnerText = "This cat looks like it's trying to eat your face.";
            ((XmlElement)toastImages[0]).SetAttribute("src", "ms-appx:///Assets/10-XAML-CatImageSmall.png");
            ((XmlElement)toastImages[0]).SetAttribute("alt", "Scary Cat Face");

            //This is the options code, which is all optional based on your needs.
            IXmlNode toastNode = toastXML.SelectSingleNode("/toast");
            
            ((XmlElement)toastNode).SetAttribute("duration", "long");

            XmlElement audioNode = toastXML.CreateElement("audio");
            audioNode.SetAttribute("src", "ms-winsoundevent:Notification.Looping.Alarm");

            //Must be used when looping audio has been selected.
            audioNode.SetAttribute("loop", "true");
            toastNode.AppendChild(audioNode);

            //You can append any text data you would like to the optional
            //launch property, but clicking a Toast message should drive
            //the user to something contextually relevant.
            ((XmlElement)toastNode).SetAttribute("launch", "<cat state='angry'><facebite state='true' /></cat>");

            ToastNotification toast = new ToastNotification(toastXML);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    }
}
