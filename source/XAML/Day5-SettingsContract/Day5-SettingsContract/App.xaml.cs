using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ApplicationSettings;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace Day5_SettingsContract
{
    sealed partial class App : Application
    {
        private Rect windowBounds;
        
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            windowBounds = Window.Current.Bounds;
            ApplySettings();

            if (rootFrame.Content == null)
            {
                
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                if (!rootFrame.Navigate(typeof(MainPage), args.Arguments))
                {
                    throw new Exception("Failed to create initial page");
                }
            }
            // Ensure the current window is active
            Window.Current.Activate();
        }

        private void ApplySettings()
        {
            SettingsPane.GetForCurrentView().CommandsRequested += (s, a) =>
            {
                SettingsCommand cmd = new SettingsCommand("foo", "Test", (handler) =>
                {
                    Popup p = new Popup();
                    p.IsLightDismissEnabled = true;
                    p.ChildTransitions = new TransitionCollection();
                    p.ChildTransitions.Add(new PaneThemeTransition()
                    {
                        Edge = (SettingsPane.Edge == SettingsEdgeLocation.Right) ?
                               EdgeTransitionLocation.Right :
                               EdgeTransitionLocation.Left
                    });

                    AboutPage about = new AboutPage();
                    about.Width = 646;
                    about.Height = windowBounds.Height;
                    p.Child = about;

                    p.SetValue(Canvas.LeftProperty, SettingsPane.Edge == SettingsEdgeLocation.Right ? (windowBounds.Width - 646) : 0);
                    p.SetValue(Canvas.TopProperty, 0);

                    p.IsOpen = true;
                });

                a.Request.ApplicationCommands.Add(cmd);
            };
        }

        void OnWindowSizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            windowBounds = Window.Current.Bounds;
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
