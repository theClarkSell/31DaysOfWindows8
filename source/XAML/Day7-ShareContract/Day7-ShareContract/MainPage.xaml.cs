using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.DataTransfer.ShareTarget;
using Windows.Data.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Day7_ShareContract
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
        /// 
ShareOperation share;
string title;
string description;
string text;
string customData;
string customDataFormat = "http://schema.org/People/";
string formattedText;
Uri uri;
IReadOnlyList<IStorageItem> storageItems;
IRandomAccessStreamReference bitmapImage;
IRandomAccessStreamReference thumbImage;
        

protected override async void OnNavigatedTo(NavigationEventArgs e)
{
    share = e.Parameter as ShareOperation;

    await Task.Factory.StartNew(async () =>
    {
        title = share.Data.Properties.Title;
        description = share.Data.Properties.Description;
        thumbImage = share.Data.Properties.Thumbnail;

        //IF THERE WAS FORMATTED TEXT SHARED
        if (share.Data.Contains(StandardDataFormats.Html))
        {
            formattedText = await share.Data.GetHtmlFormatAsync();
        }
        //IF THERE WAS A URI SHARED
        if (share.Data.Contains(StandardDataFormats.Uri))
        {
            uri = await share.Data.GetUriAsync();
        }
        //IF THERE WAS UNFORMATTED TEXT SHARED
        if (share.Data.Contains(StandardDataFormats.Text))
        {
            text = await share.Data.GetTextAsync();
        }
        //IF THERE WERE FILES SHARED
        if (share.Data.Contains(StandardDataFormats.StorageItems))
        {
            storageItems = await share.Data.GetStorageItemsAsync();
        }
        //IF THERE WAS CUSTOM DATA SHARED
        if (share.Data.Contains(customDataFormat))
        {
            customData = await share.Data.GetTextAsync(customDataFormat);
        }
        //IF THERE WERE IMAGES SHARED.
        if (share.Data.Contains(StandardDataFormats.Bitmap))
        {
            bitmapImage = await share.Data.GetBitmapAsync();
        }

        //MOVING BACK TO THE UI THREAD, THIS IS WHERE WE POPULATE OUR INTERFACE.
        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                TitleBox.Text = title;
                DescriptionBox.Text = description;

                if (text != null)
                    UnformattedTextBox.Text = text;
                if (uri != null)
                {
                    UriButton.Content = uri.ToString();
                    UriButton.NavigateUri = uri;
                }

                if (formattedText != null)
                    HTMLTextBox.NavigateToString(HtmlFormatHelper.GetStaticFragment(formattedText));

                if (bitmapImage != null)
                {
                    IRandomAccessStreamWithContentType bitmapStream = await this.bitmapImage.OpenReadAsync();
                    BitmapImage bi = new BitmapImage();
                    bi.SetSource(bitmapStream);
                    WholeImage.Source = bi;

                    bitmapStream = await this.thumbImage.OpenReadAsync();
                    bi = new BitmapImage();
                    bi.SetSource(bitmapStream);
                    ThumbImage.Source = bi;
                }

                if (customData != null)
                {
                    StringBuilder receivedStrings = new StringBuilder();
                    JsonObject customObject = JsonObject.Parse(customData);
                    if (customObject.ContainsKey("type"))
                    {
                        if (customObject["type"].GetString() == "http://schema.org/Person")
                        {
                            receivedStrings.AppendLine("Type: " + customObject["type"].Stringify());
                            JsonObject properties = customObject["properties"].GetObject();
                            receivedStrings.AppendLine("Image: " + properties["image"].Stringify());
                            receivedStrings.AppendLine("Name: " + properties["name"].Stringify());
                            receivedStrings.AppendLine("Affiliation: " + properties["affiliation"].Stringify());
                            receivedStrings.AppendLine("Birth Date: " + properties["birthDate"].Stringify());
                            receivedStrings.AppendLine("Job Title: " + properties["jobTitle"].Stringify());
                            receivedStrings.AppendLine("Nationality: " + properties["Nationality"].Stringify());
                            receivedStrings.AppendLine("Gender: " + properties["gender"].Stringify());
                        }
                        CustomDataBox.Text = receivedStrings.ToString();
                    }
                }
            });
        });

}

        private void PopulateScreen()
        {
            
        }

        private void UnformattedTextButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(UnformattedTextSource));
        }

        private void LinkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LinkSource));
        }

        private void FormattedTextButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(FormattedTextSource));
        }

        private void FileButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(FileSource));
        }

        private void ImageButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ImageSource));
        }

        private void CustomDataButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CustomDataSource));
        }
    }
}
