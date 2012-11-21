using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.PlayTo;
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

namespace Day22_PlayTo
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

        PlayToManager manager = null;
        CoreDispatcher dispatcher = null;
        PlayToReceiver receiver = null;

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            dispatcher = Window.Current.CoreWindow.Dispatcher;
            manager = PlayToManager.GetForCurrentView();
            manager.SourceRequested += manager_SourceRequested;

            receiver = new PlayToReceiver();
            receiver.PlayRequested += receiver_PlayRequested;
            receiver.PauseRequested += receiver_PauseRequested;
            receiver.StopRequested += receiver_StopRequested;
            receiver.MuteChangeRequested += receiver_MuteChangeRequested;
            receiver.VolumeChangeRequested += receiver_VolumeChangeRequested;
            receiver.TimeUpdateRequested += receiver_TimeUpdateRequested;
            receiver.CurrentTimeChangeRequested += receiver_CurrentTimeChangeRequested;
            receiver.SourceChangeRequested += receiver_SourceChangeRequested;
            receiver.PlaybackRateChangeRequested += receiver_PlaybackRateChangeRequested;
            receiver.SupportsAudio = true;
            receiver.SupportsVideo = true;
            receiver.SupportsImage = true;

            receiver.FriendlyName = "Day #22 - PlayTo";

            await receiver.StartAsync();
        }

        private async void receiver_PlaybackRateChangeRequested(PlayToReceiver sender, PlaybackRateChangeRequestedEventArgs args)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
            {
                VideoSource.PlaybackRate = args.Rate;
            });
        }

        private async void receiver_CurrentTimeChangeRequested(PlayToReceiver sender, CurrentTimeChangeRequestedEventArgs args)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
            {
                if (VideoSource.CanSeek)
                {
                    {
                        VideoSource.Position = args.Time;
                        receiver.NotifySeeking();
                    }
                }
            });
        }

        private async void receiver_TimeUpdateRequested(PlayToReceiver sender, object args)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
            {
                receiver.NotifyTimeUpdate(VideoSource.Position);
            });
        }

        private async void receiver_VolumeChangeRequested(PlayToReceiver sender, VolumeChangeRequestedEventArgs args)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
            {
                VideoSource.Volume = args.Volume;
            });
        }

        private async void receiver_MuteChangeRequested(PlayToReceiver sender, MuteChangeRequestedEventArgs args)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
            {
                VideoSource.IsMuted = args.Mute;
            });
        }

        async void receiver_SourceChangeRequested(PlayToReceiver sender, SourceChangeRequestedEventArgs args)
        {
            if (args.Stream != null)
            {
                if (args.Stream.ContentType.Contains("image"))
                {
                    await Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
                    {
                        BitmapImage bmp = new BitmapImage();
                        bmp.SetSource(args.Stream);
                        PhotoSource.Source = bmp;
                        ShowSelectedPanel(1);
                    });
                }
                else if (args.Stream.ContentType.Contains("video"))
                {
                    await Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
                    {
                        VideoSource.SetSource(args.Stream, args.Stream.ContentType);
                        ShowSelectedPanel(3);
                    });
                }
                else if (args.Stream.ContentType.Contains("audio"))
                {
                    await Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
                    {
                        MusicSource.SetSource(args.Stream, args.Stream.ContentType);
                        ShowSelectedPanel(2);
                        MusicSource.Play();
                    });
                }
            }
        }

        async void receiver_StopRequested(PlayToReceiver sender, object args)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
            {
                MusicSource.Stop();
                VideoSource.Stop();
                receiver.NotifyStopped();
            });
        }

        async void receiver_PauseRequested(PlayToReceiver sender, object args)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
            {
                MusicSource.Pause();
                VideoSource.Pause();
                receiver.NotifyPaused();
            });
        }

        async void receiver_PlayRequested(PlayToReceiver sender, object args)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
            {
                MusicSource.Play();
                VideoSource.Play();
                receiver.NotifyPlaying();
            });
        }

        protected async override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            manager.SourceRequested -= manager_SourceRequested;
            //await receiver.StopAsync();
        }

        void manager_SourceRequested(PlayToManager sender, PlayToSourceRequestedEventArgs args)
        {
            var deferral = args.SourceRequest.GetDeferral();
            var handler = dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                if (MusicBox.Visibility == Visibility.Visible)
                {
                    args.SourceRequest.SetSource(MusicSource.PlayToSource);
                }
                else if (VideoBox.Visibility == Visibility.Visible)
                {
                    args.SourceRequest.SetSource(VideoSource.PlayToSource);
                }
                else if (PhotoSource.Visibility == Visibility.Visible)
                {
                    args.SourceRequest.SetSource(PhotoSource.PlayToSource);
                }
                deferral.Complete();
            });
        }

        private void ShowSelectedPanel(int p)
        {
            switch (p)
            {
                case 1:
                    VideoSource.Stop();
                    MusicSource.Stop();

                    MusicBox.Visibility = Visibility.Collapsed;
                    PhotoSource.Visibility = Visibility.Visible;
                    VideoBox.Visibility = Visibility.Collapsed;
                    break;
                case 2:
                    VideoSource.Stop();
            
                    MusicBox.Visibility = Visibility.Visible;
                    PhotoSource.Visibility = Visibility.Collapsed;
                    VideoBox.Visibility = Visibility.Collapsed;

                    MusicSource.Play();
                    break;
                case 3:
                    MusicSource.Stop();
                    MusicBox.Visibility = Visibility.Collapsed;
                    PhotoSource.Visibility = Visibility.Collapsed;
                    VideoBox.Visibility = Visibility.Visible;

                    VideoSource.Play();
                    break;
            }
        }

        private void Video_Clicked(object sender, TappedRoutedEventArgs e)
        {
            ShowSelectedPanel(3);
        }

        private void Photo_Clicked(object sender, TappedRoutedEventArgs e)
        {
            ShowSelectedPanel(1);
        }

        private void Music_Clicked(object sender, TappedRoutedEventArgs e)
        {
            ShowSelectedPanel(2);
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            VideoSource.Play();
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            VideoSource.Pause();
        }

        private void PauseMusic_Click(object sender, RoutedEventArgs e)
        {
            MusicSource.Pause();
        }

        private void PlayMusic_Click(object sender, RoutedEventArgs e)
        {
            MusicSource.Play();
        }
    }
}
