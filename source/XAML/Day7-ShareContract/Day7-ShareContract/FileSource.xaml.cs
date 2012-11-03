using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.ApplicationModel;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Day7_ShareContract
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class FileSource : Day7_ShareContract.Common.LayoutAwarePage
    {
        public FileSource()
        {
            this.InitializeComponent();
        }

        DataTransferManager dtm;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            dtm = DataTransferManager.GetForCurrentView();
            dtm.DataRequested += dtm_DataRequested;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            dtm.DataRequested -= dtm_DataRequested;
        }

        async void dtm_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            string FileTitle = "31 Days of Windows 8!";
            string FileDescription = "This just explains what we're sharing.";  //This is an optional value.

            DataPackage data = args.Request.Data;
            data.Properties.Title = FileTitle;
            data.Properties.Description = FileDescription;

            DataRequestDeferral waiter = args.Request.GetDeferral();

            try
            {
                StorageFile textFile = await Package.Current.InstalledLocation.GetFileAsync("FileForSharing.txt");
                List<IStorageItem> files = new List<IStorageItem>();
                files.Add(textFile);
                data.SetStorageItems(files);
            }
            finally
            {
                waiter.Complete();
            }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }
    }
}
