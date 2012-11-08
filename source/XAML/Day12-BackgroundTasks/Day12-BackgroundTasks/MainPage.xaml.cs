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
using Windows.ApplicationModel.Background;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Day12_BackgroundTasks
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

        bool isTaskRegistered = false;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            CheckTaskRegistration();
        }

        private void CheckTaskRegistration()
        {
            foreach (var task in BackgroundTaskRegistration.AllTasks)
            {
                if (task.Value.Name == "TileUpdater")
                {
                    isTaskRegistered = true;
                    break;
                }
            }

            if (isTaskRegistered)
            {
                RegisterButton.Content = "Unregister Background Task";
            }
            else if (!isTaskRegistered)
            {
                RegisterButton.Content = "Register Background Task";
            }
        }

        private void RegisterBackgroundTask(string name, string entrypoint)
        {
            BackgroundTaskBuilder btb = new BackgroundTaskBuilder();
            btb.Name = name;
            btb.TaskEntryPoint = entrypoint;
            btb.SetTrigger(new SystemTrigger(SystemTriggerType.InternetAvailable, false));

            BackgroundTaskRegistration task = btb.Register();

            task.Progress += task_Progress;
            task.Completed += task_Completed;
        }

        void task_Completed(BackgroundTaskRegistration sender, BackgroundTaskCompletedEventArgs args)
        {
            //IMPLEMENT SOME CODE TO RUN AFTER YOUR BACKGROUND AGENT COMPLETES.
        }

        void task_Progress(BackgroundTaskRegistration sender, BackgroundTaskProgressEventArgs args)
        {
            
        }

        private void UnregisterBackgroundTask(string name)
        {
            foreach (var task in BackgroundTaskRegistration.AllTasks)
            {
                if (task.Value.Name == name)
                {
                    task.Value.Unregister(true);
                }
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (isTaskRegistered)
            {
                UnregisterBackgroundTask("TileUpdater");
                isTaskRegistered = false;

            }
            else
            {
                RegisterBackgroundTask("TileUpdater", "BackgroundTasks.TileUpdater");
            }

            CheckTaskRegistration();
        }
    }
}
