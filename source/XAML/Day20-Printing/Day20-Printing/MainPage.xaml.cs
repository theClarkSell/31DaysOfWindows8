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

        PrintDocument document;
        IPrintDocumentSource source;
        List<UIElement> printPreviewPages;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //document = new PrintDocument();
            //source = document.DocumentSource;

            //document.AddPages += document_AddPages;
            //document.GetPreviewPage += document_GetPreviewPage;
            //document.Paginate += document_Paginate;

            PrintManager manager = PrintManager.GetForCurrentView();
            manager.PrintTaskRequested += PrintTaskRequested;
            
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            //document.AddPages -= document_AddPages;
            //document.GetPreviewPage -= document_GetPreviewPage;
            //document.Paginate -= document_Paginate;
            
            PrintManager manager = PrintManager.GetForCurrentView();
            manager.PrintTaskRequested -= PrintTaskRequested;
            

        }

        

        private async  void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            await Windows.Graphics.Printing.PrintManager.ShowPrintUIAsync();
            
        }

        private void CreatePrintContent()
        {
            PrintingPage page = new PrintingPage();
            PrintingContainer.Children.Add(page);
            PrintingContainer.UpdateLayout();
            
        }

        void document_Paginate(object sender, PaginateEventArgs e)
        {
            
        }

        void document_GetPreviewPage(object sender, GetPreviewPageEventArgs e)
        {
            
        }

        void document_AddPages(object sender, AddPagesEventArgs e)
        {
            
        }

        private void PrintTaskRequested(PrintManager sender, PrintTaskRequestedEventArgs args)
        {
            PrintTask printTask = null;
            printTask = args.Request.CreatePrintTask("Simple Print Job", sourceRequested =>
            {
                sourceRequested.SetSource(source);
            });
        }

        

        

        
    }
}
