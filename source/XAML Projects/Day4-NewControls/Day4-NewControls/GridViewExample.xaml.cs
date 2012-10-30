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

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Day4_NewControls
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class GridViewExample : Day4_NewControls.Common.LayoutAwarePage
    {
        public GridViewExample()
        {
            this.InitializeComponent();
        }

protected override void OnNavigatedTo(NavigationEventArgs e)
{
    base.OnNavigatedTo(e);

    List<Player> players = new List<Player>();
    players.Add(new Player { Name = "Frank Richard", TeamName = "Albany Old Schoolers", Height=74.2, Weight=245, Number=03});
    players.Add(new Player { Name = "Buddy Hobbs", TeamName = "New York Elves", Height = 76.8, Weight = 227, Number = 04 });
    players.Add(new Player { Name = "Ron Burgundy", TeamName = "San Diego Anchormen", Height = 77.1, Weight = 275, Number = 05 });
    players.Add(new Player { Name = "Ricky Bobby", TeamName = "Talladega Racers", Height = 70.6, Weight = 198, Number = 06 });
    players.Add(new Player { Name = "Chazz Reinhold", TeamName = "Washington Crashers", Height = 70.6, Weight = 198, Number = 06 });

    mainGridView.ItemsSource = players;

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
