using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Day16_ContextMenu
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
            Lipsum.ContextMenuOpening += Lipsum_ContextMenuOpening;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            Lipsum.ContextMenuOpening -= Lipsum_ContextMenuOpening;
        }

        async void Lipsum_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            e.Handled = true;
            TextBox t = (TextBox)sender;

            PopupMenu p = new PopupMenu();
            p.Commands.Add(new UICommand("Cut", null, 0));
            p.Commands.Add(new UICommand("Copy", null, 1));
            p.Commands.Add(new UICommand("Paste", null, 2));
            p.Commands.Add(new UICommand("Select All", null, 3));
            p.Commands.Add(new UICommandSeparator());
            p.Commands.Add(new UICommand("Delete", null, 4));

            var selectedCommand = await p.ShowForSelectionAsync(GetTextBoxRect(t));

            if (selectedCommand != null)
            {
                String text;
                DataPackage d;
                
                switch ((int)selectedCommand.Id)
                {
                    case 0: //CUT
                        text = t.SelectedText;
                        t.SelectedText = "";
                        d = new DataPackage();
                        d.SetText(text);
                        Clipboard.SetContent(d);
                        break;
                    case 1: //COPY
                        text = t.SelectedText;
                        d = new DataPackage();
                        d.SetText(text);
                        Clipboard.SetContent(d);
                        break;
                    case 2: //PASTE
                        text = await Clipboard.GetContent().GetTextAsync();
                        t.SelectedText = text;
                        break;
                    case 3: //SELECT ALL
                        t.SelectAll();
                        break;
                    case 4: //DELETE
                        t.SelectedText = "";
                        break;
                }
            }
        }

        private Rect GetTextBoxRect(TextBox t)
        {
            Rect temp = t.GetRectFromCharacterIndex(t.SelectionStart, false);

            GeneralTransform transform = t.TransformToVisual(null);
            Point point = transform.TransformPoint(new Point());
            point.X = point.X + temp.X;
            point.Y = point.Y + temp.Y;

            return new Rect(point, new Size(temp.Width, temp.Height));
        }

        private async void Element_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            PopupMenu p = new PopupMenu();
            p.Commands.Add(new UICommand("Blankenburg", (command) => { ((Grid)Logo.Parent).Background = new SolidColorBrush(Colors.Red); }));
            p.Commands.Add(new UICommand("31 Days of Windows 8", (command) => { ((Grid)Logo.Parent).Background = new SolidColorBrush(Colors.Orange); }));
            p.Commands.Add(new UICommandSeparator());
            //p.Commands.Add(new UICommand("Share", (command) => { ((Grid)Logo.Parent).Background = new SolidColorBrush(Colors.Yellow); }));
            p.Commands.Add(new UICommand("Open With...", (command) => { ((Grid)Logo.Parent).Background = new SolidColorBrush(Colors.Green); }));
            p.Commands.Add(new UICommand("Delete", (command) => { ((Grid)Logo.Parent).Background = new SolidColorBrush(Colors.Blue); }));
            p.Commands.Add(new UICommand("Hide With Rectangle", (command) => { HideWithRectangle(sender); }));
            await p.ShowForSelectionAsync(GetRect(sender), Placement.Right);
        }

        private Rect GetRect(object sender)
        {
            FrameworkElement element = sender as FrameworkElement;
            GeneralTransform elementTransform = element.TransformToVisual(null);
            Point point = elementTransform.TransformPoint(new Point());
            return new Rect(point, new Size(element.ActualWidth, element.ActualHeight));
        }

        private void HideWithRectangle(object sender)
        {
            FrameworkElement element = sender as FrameworkElement;
            Rectangle r = new Rectangle { Width = element.ActualWidth, Height = element.ActualHeight, Fill = new SolidColorBrush(Colors.White) };
            Grid.SetColumn(r, Grid.GetColumn(element));
            Grid.SetRow(r, Grid.GetRow(element));
            r.Margin = element.Margin;
            ((Grid)element.Parent).Children.Add(r);
        }
    }
}
