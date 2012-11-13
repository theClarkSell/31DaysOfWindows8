using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Data.Html;
using Windows.Storage.Streams;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;
using Windows.System;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Day17_Clipboard
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        DataPackage d;
        
        public MainPage()
        {
            this.InitializeComponent();
            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            HTMLSource.Navigate(new Uri("ms-appx-web:///SampleHTML.html"));
            d = new DataPackage();
        }

        private void TextButton_Click(object sender, RoutedEventArgs e)
        {
            d.SetText(TextBoxValue.Text);
            Clipboard.SetContent(d);
        }

        private void HTMLButton_Click(object sender, RoutedEventArgs e)
        {
            string s = HtmlFormatHelper.CreateHtmlFormat(HTMLSource.InvokeScript("eval", new string[] { "document.documentElement.outerHTML;" }));
            d.SetHtmlFormat(s);

            string t = HtmlUtilities.ConvertToText(s);
            d.SetText(t);

            Clipboard.SetContent(d);
        }

        private async void ImageButton_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("ms-appx:///Assets/WideLogo.png");
            StorageFile sf = await StorageFile.GetFileFromApplicationUriAsync(uri);
            d.SetBitmap(RandomAccessStreamReference.CreateFromFile(sf));
            Clipboard.SetContent(d);
        }

        private async void FileButton_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("ms-appx:///Assets/PeriodicTable.xls");
            List<StorageFile> files = new List<StorageFile>();
            StorageFile sf = await StorageFile.GetFileFromApplicationUriAsync(uri);
            files.Add(sf);
            d.SetStorageItems(files);
            Clipboard.SetContent(d);
        }

        private async void PasteButton_Click(object sender, RoutedEventArgs e)
        {
            var DataPackage = Clipboard.GetContent();

            if (DataPackage.Contains(StandardDataFormats.Text))
            {
                TextBoxPaste.Text = await DataPackage.GetTextAsync();
            }

            if (DataPackage.Contains(StandardDataFormats.Bitmap))
            {
                RandomAccessStreamReference image = await DataPackage.GetBitmapAsync();
                var imageStream = await image.OpenReadAsync();
                BitmapImage bmp = new BitmapImage();
                bmp.SetSource(imageStream);
                ImagePaste.Source = bmp;
            }

            if (DataPackage.Contains(StandardDataFormats.StorageItems))
            {
                var storageFiles = await DataPackage.GetStorageItemsAsync();

                foreach (var file in storageFiles)
                {
                    var currentFile = file as StorageFile;
                    await currentFile.CopyAsync(ApplicationData.Current.LocalFolder, currentFile.Name, NameCollisionOption.ReplaceExisting);
                    MessageText.Text = currentFile.Name + " has been saved to " + ApplicationData.Current.LocalFolder.Path.ToString() + "/" + currentFile.Name;
                }
            }

            if (DataPackage.Contains(StandardDataFormats.Html))
            {
                string html = await DataPackage.GetHtmlFormatAsync();
                HTMLPaste.NavigateToString(html);
            }
        }
    }
}
