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

namespace Day29_ApplicationLifecycle
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        ApplicationDataContainer settings;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Application.Current.Suspending += Current_Suspending;
            Application.Current.Resuming += Current_Resuming;
            settings = ApplicationData.Current.LocalSettings;
        }

        void Current_Suspending(object sender, Windows.ApplicationModel.SuspendingEventArgs e)
        {
            settings.Values["suspendedDateTime"] = DateTime.Now.ToString();
            settings.Values["customTextValue"] = CustomText.Text;
        }

        void Current_Resuming(object sender, object e)
        {
            Message.Text = "Resumed.  Was suspended at\n\n" + settings.Values["suspendedDateTime"];
            CustomText.Text = "JJ" + settings.Values["customTextValue"].ToString();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            Application.Current.Resuming -= Current_Resuming;
            Application.Current.Suspending += Current_Suspending;
        }

    }
}
