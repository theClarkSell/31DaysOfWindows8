using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Printing;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Printing;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Day20_Printing
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

        PrintDocument document = null;
        IPrintDocumentSource source = null;
        List<UIElement> pages = null;
        FrameworkElement page1;
        protected event EventHandler pagesCreated;
        protected const double left = 0.075;
        protected const double top = 0.03;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            document = new PrintDocument();
            source = document.DocumentSource;

            document.Paginate += printDocument_Paginate;
            document.GetPreviewPage += printDocument_GetPreviewPage;
            document.AddPages += printDocument_AddPages;

            PrintManager manager = PrintManager.GetForCurrentView();
            manager.PrintTaskRequested += manager_PrintTaskRequested;

            pages = new List<UIElement>();

            PrepareContent();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            if (document == null) return;

            document.Paginate -= printDocument_Paginate;
            document.GetPreviewPage -= printDocument_GetPreviewPage;
            document.AddPages -= printDocument_AddPages;

            // Remove the handler for printing initialization.
            PrintManager manager = PrintManager.GetForCurrentView();
            manager.PrintTaskRequested -= manager_PrintTaskRequested;

            PrintContainer.Children.Clear();
        }

        private void PrepareContent()
        {
            if (page1 == null)
            {
                page1 = new PageForPrinting();
                StackPanel header = (StackPanel)page1.FindName("header");
                header.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }

            PrintContainer.Children.Add(page1);
            PrintContainer.InvalidateMeasure();
            PrintContainer.UpdateLayout();
        }

        void manager_PrintTaskRequested(PrintManager sender, PrintTaskRequestedEventArgs args)
        {
            PrintTask task = null;
            task = args.Request.CreatePrintTask("Day #20 - Simple Print Job", sourceRequested =>
            {
                sourceRequested.SetSource(source);
            });
        }

        void printDocument_AddPages(object sender, AddPagesEventArgs e)
        {
            for (int i = 0; i < pages.Count; i++)
            {
                document.AddPage(pages[i]);
            }

            PrintDocument printDoc = (PrintDocument)sender;
            printDoc.AddPagesComplete();
        }

        void printDocument_GetPreviewPage(object sender, GetPreviewPageEventArgs e)
        {
            PrintDocument printDoc = (PrintDocument)sender;

            printDoc.SetPreviewPage(e.PageNumber, pages[e.PageNumber - 1]);
        }

        void printDocument_Paginate(object sender, PaginateEventArgs e)
        {
            pages.Clear();
            PrintContainer.Children.Clear();

            RichTextBlockOverflow lastRTBOOnPage;
            PrintTaskOptions printingOptions = ((PrintTaskOptions)e.PrintTaskOptions);
            PrintPageDescription pageDescription = printingOptions.GetPageDescription(0);

            lastRTBOOnPage = AddOnePrintPreviewPage(null, pageDescription);

            while (lastRTBOOnPage.HasOverflowContent && lastRTBOOnPage.Visibility == Windows.UI.Xaml.Visibility.Visible)
            {
                lastRTBOOnPage = AddOnePrintPreviewPage(lastRTBOOnPage, pageDescription);
            }

            if (pagesCreated != null)
            {
                pagesCreated.Invoke(pages, null);
            }

            PrintDocument printDoc = (PrintDocument)sender;

            printDoc.SetPreviewPageCount(pages.Count, PreviewPageCountType.Intermediate);
        }

        private RichTextBlockOverflow AddOnePrintPreviewPage(RichTextBlockOverflow lastRTBOAdded, PrintPageDescription printPageDescription)
        {
            FrameworkElement page;
            RichTextBlockOverflow link;

            if (lastRTBOAdded == null)
            {
                page = page1;
                StackPanel footer = (StackPanel)page.FindName("footer");
                footer.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
            else
            {
                page = new ContinuationPage(lastRTBOAdded);
            }

            page.Width = printPageDescription.PageSize.Width;
            page.Height = printPageDescription.PageSize.Height;

            Grid printableArea = (Grid)page.FindName("printableArea");

            double marginWidth = Math.Max(printPageDescription.PageSize.Width - printPageDescription.ImageableRect.Width, printPageDescription.PageSize.Width * left * 2);
            double marginHeight = Math.Max(printPageDescription.PageSize.Height - printPageDescription.ImageableRect.Height, printPageDescription.PageSize.Height * top * 2);

            printableArea.Width = page1.Width - marginWidth;
            printableArea.Height = page1.Height - marginHeight;
          
            PrintContainer.Children.Add(page);
            PrintContainer.InvalidateMeasure();
            PrintContainer.UpdateLayout();

            // Find the last text container and see if the content is overflowing
            link = (RichTextBlockOverflow)page.FindName("continuationPageLinkedContainer");

            // Check if this is the last page
            if (!link.HasOverflowContent && link.Visibility == Windows.UI.Xaml.Visibility.Visible)
            {
                StackPanel footer = (StackPanel)page.FindName("footer");
                footer.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }

            // Add the page to the page preview collection
            pages.Add(page);

            return link;
        }
    }
}
