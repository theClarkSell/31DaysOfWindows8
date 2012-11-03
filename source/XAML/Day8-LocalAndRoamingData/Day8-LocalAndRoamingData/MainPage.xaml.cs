using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Day8_LocalAndRoamingData
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ApplicationDataContainer settingsLocal;
        ApplicationDataContainer settingsRoaming;

        string currentBook;
        int currentPage;
        
        public MainPage()
        {
            this.InitializeComponent();

            settingsLocal = ApplicationData.Current.LocalSettings;
            settingsRoaming = ApplicationData.Current.RoamingSettings;

            AddSettings();
        }

        private void AddSettings()
        {
            //There is no reason to set the same data to both local and roaming.
            //This is here merely for illustration of HOW to do it.
            //You should make the choice as to whether your data should be roamed.

            settingsLocal.Values["currentBook"] = "Hitchhiker's Guide To The Galaxy";
            settingsLocal.Values["currentPage"] = 42;

            settingsRoaming.Values["currentBook"] = "Hitchhiker's Guide To The Galaxy";
            settingsRoaming.Values["currentPage"] = 42;

            ReadSettings();
        }

        private void ReadSettings()
        {
            //If you want typed data when you read it out of settings,
            //you're going to need to know what it is, and cast it.

            currentBook = (string)settingsLocal.Values["currentBook"];
            currentPage = (int)settingsRoaming.Values["currentPage"];
            
            DeleteSettings();
        }

        private void DeleteSettings()
        {
            settingsLocal.Values.Remove("currentBook");
            settingsLocal.Values.Remove("currentPage");

            settingsRoaming.Values.Remove("currentBook");
            settingsRoaming.Values.Remove("currentPage");

            AddCategoryAndNewSettingValue();
        }

        private void AddCategoryAndNewSettingValue()
        {
            settingsLocal.CreateContainer("mediaSettings", ApplicationDataCreateDisposition.Always);
            settingsLocal.Containers["mediaSettings"].Values["Volume"] = 11;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }
    }
}
