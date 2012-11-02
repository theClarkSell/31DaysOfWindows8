using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Search Contract item template is documented at http://go.microsoft.com/fwlink/?LinkId=234240

namespace Day6_SearchContract
{
    /// <summary>
    /// This page displays search results when a global search is directed to this application.
    /// </summary>
    public sealed partial class SearchResults : Day6_SearchContract.Common.LayoutAwarePage
    {
        List<Element> elements = new List<Element>();
        IEnumerable<SearchResult> searchResults;
        string searchString;

        public SearchResults()
        {
            this.InitializeComponent();
            BuildElementList();
        }

        private void BuildElementList()
        {
            elements.Add(new Element { AtomicNumber = 1, AtomicWeight = 1.01, Category = "Alkali Metals", Name = "Hydrogen", Symbol = "H", State = "Gas" });
            elements.Add(new Element { AtomicNumber = 2, AtomicWeight = 4.003, Category = "Noble Gases", Name = "Helium", Symbol = "He", State = "Gas" });
            elements.Add(new Element { AtomicNumber = 3, AtomicWeight = 6.94, Category = "Alkali Metals", Name = "Lithium", Symbol = "Li", State = "Solid" });
            elements.Add(new Element { AtomicNumber = 4, AtomicWeight = 9.01, Category = "Alkaline Earth Metals", Name = "Beryllium", Symbol = "Be", State = "Solid" });
            elements.Add(new Element { AtomicNumber = 5, AtomicWeight = 10.81, Category = "Non Metals", Name = "Boron", Symbol = "B", State = "Solid" });
            elements.Add(new Element { AtomicNumber = 6, AtomicWeight = 12.01, Category = "Non Metals", Name = "Carbon", Symbol = "C", State = "Solid" });
            elements.Add(new Element { AtomicNumber = 7, AtomicWeight = 14.01, Category = "Non Metals", Name = "Nitrogen", Symbol = "N", State = "Gas" });
            elements.Add(new Element { AtomicNumber = 8, AtomicWeight = 15.999, Category = "Non Metals", Name = "Oxygen", Symbol = "O", State = "Gas" });
            elements.Add(new Element { AtomicNumber = 9, AtomicWeight = 18.998, Category = "Non Metals", Name = "Fluorine", Symbol = "F", State = "Gas" });
            elements.Add(new Element { AtomicNumber = 10, AtomicWeight = 20.18, Category = "Noble Gases", Name = "Neon", Symbol = "Ne", State = "Gas" });
            elements.Add(new Element { AtomicNumber = 11, AtomicWeight = 22.99, Category = "Alkali Metals", Name = "Sodium", Symbol = "Na", State = "Solid" });
            elements.Add(new Element { AtomicNumber = 12, AtomicWeight = 24.31, Category = "Alkaline Earth Metals", Name = "Magnesium", Symbol = "Mg", State = "Solid" });
            elements.Add(new Element { AtomicNumber = 13, AtomicWeight = 26.98, Category = "Other Metals", Name = "Aluminum", Symbol = "Al", State = "Solid" });
            elements.Add(new Element { AtomicNumber = 14, AtomicWeight = 28.09, Category = "Non Metals", Name = "Silicon", Symbol = "Si", State = "Solid" });
            elements.Add(new Element { AtomicNumber = 15, AtomicWeight = 30.97, Category = "Non Metals", Name = "Phosphorus", Symbol = "P", State = "Solid" });
            elements.Add(new Element { AtomicNumber = 16, AtomicWeight = 32.06, Category = "Non Metals", Name = "Sulfur", Symbol = "S", State = "Solid" });
            elements.Add(new Element { AtomicNumber = 17, AtomicWeight = 35.45, Category = "Non Metals", Name = "Chlorine", Symbol = "Cl", State = "Gas" });
            elements.Add(new Element { AtomicNumber = 18, AtomicWeight = 39.95, Category = "Noble Gases", Name = "Argon", Symbol = "Ar", State = "Gas" });
            elements.Add(new Element { AtomicNumber = 19, AtomicWeight = 39.10, Category = "Alkali Metals", Name = "Potassium", Symbol = "K", State = "Solid" });
            elements.Add(new Element { AtomicNumber = 20, AtomicWeight = 40.08, Category = "Alkaline Earth Metals", Name = "Calcium", Symbol = "Ca", State = "Solid" });
            elements.Add(new Element { AtomicNumber = 21, AtomicWeight = 44.96, Category = "Transitional Metals", Name = "Scandium", Symbol = "Sc", State = "Solid" });
            elements.Add(new Element { AtomicNumber = 22, AtomicWeight = 47.90, Category = "Transitional Metals", Name = "Titanium", Symbol = "Ti", State = "Solid" });
            elements.Add(new Element { AtomicNumber = 23, AtomicWeight = 50.94, Category = "Transitional Metals", Name = "Vanadium", Symbol = "V", State = "Solid" });
            elements.Add(new Element { AtomicNumber = 24, AtomicWeight = 51.996, Category = "Transitional Metals", Name = "Chromium", Symbol = "Cr", State = "Solid" });
            elements.Add(new Element { AtomicNumber = 25, AtomicWeight = 54.94, Category = "Transitional Metals", Name = "Manganese", Symbol = "Mn", State = "Solid" });
            elements.Add(new Element { AtomicNumber = 26, AtomicWeight = 55.85, Category = "Transitional Metals", Name = "Iron", Symbol = "Fe", State = "Solid" });
            elements.Add(new Element { AtomicNumber = 27, AtomicWeight = 58.93, Category = "Transitional Metals", Name = "Cobalt", Symbol = "Co", State = "Solid" });
            elements.Add(new Element { AtomicNumber = 28, AtomicWeight = 58.70, Category = "Transitional Metals", Name = "Nickel", Symbol = "Ni", State = "Solid" });
            elements.Add(new Element { AtomicNumber = 29, AtomicWeight = 63.55, Category = "Transitional Metals", Name = "Copper", Symbol = "Cu", State = "Solid" });
            elements.Add(new Element { AtomicNumber = 30, AtomicWeight = 65.37, Category = "Transitional Metals", Name = "Zinc", Symbol = "Zn", State = "Solid" });
            elements.Add(new Element { AtomicNumber = 31, AtomicWeight = 69.72, Category = "Other Metals", Name = "Gallium", Symbol = "Ga", State = "Solid" });
            elements.Add(new Element { AtomicNumber = 32, AtomicWeight = 72.59, Category = "Other Metals", Name = "Germanium", Symbol = "Ge", State = "Solid" });
            elements.Add(new Element { AtomicNumber = 33, AtomicWeight = 74.92, Category = "Non Metals", Name = "Arsenic", Symbol = "As", State = "Solid" });
            elements.Add(new Element { AtomicNumber = 34, AtomicWeight = 78.96, Category = "Non Metals", Name = "Selenium", Symbol = "Se", State = "Solid" });
            elements.Add(new Element { AtomicNumber = 35, AtomicWeight = 79.90, Category = "Non Metals", Name = "Bromine", Symbol = "Br", State = "Liquid" });
            elements.Add(new Element { AtomicNumber = 36, AtomicWeight = 83.80, Category = "Noble Gases", Name = "Krypton", Symbol = "Kr", State = "Gas" });

            searchResults = from el in elements
                            where el.Name.Contains(searchString)
                            orderby el.Name ascending
                            select new SearchResult
                            {
                                //Image = new BitmapImage(new Uri("Assets/StoreLogo.png", UriKind.Relative)),
                                Description = el.Name,
                                Title = el.Symbol
                                //Subtitle = el.Category
                            };
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
            var queryText = navigationParameter as String;
            searchString = queryText;

            

            var filterList = new List<Filter>();
            filterList.Add(new Filter("All", searchResults.Count(), true));

            // Communicate results through the view model
            this.DefaultViewModel["QueryText"] = '\u201c' + queryText + '\u201d';
            this.DefaultViewModel["Filters"] = filterList;
            this.DefaultViewModel["ShowFilters"] = filterList.Count > 1;
            
            
        }



        /// <summary>
        /// Invoked when a filter is selected using the ComboBox in snapped view state.
        /// </summary>
        /// <param name="sender">The ComboBox instance.</param>
        /// <param name="e">Event data describing how the selected filter was changed.</param>
        void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Determine what filter was selected
            var selectedFilter = e.AddedItems.FirstOrDefault() as Filter;
            if (selectedFilter != null)
            {
                // Mirror the results into the corresponding Filter object to allow the
                // RadioButton representation used when not snapped to reflect the change
                selectedFilter.Active = true;

                // TODO: Respond to the change in active filter by setting this.DefaultViewModel["Results"]
                //       to a collection of items with bindable Image, Title, Subtitle, and Description properties
                
                this.DefaultViewModel["Results"] = searchResults;

                // Ensure results are found
                object results;
                IEnumerable<SearchResult> resultsCollection;

                if (this.DefaultViewModel.TryGetValue("Results", out results))
                {
                    if ((resultsCollection = results as IEnumerable<SearchResult>) != null)
                    {
                        if (resultsCollection.Count() != 0)
                        {
                            VisualStateManager.GoToState(this, "ResultsFound", true);
                            return;
                        }
                    }
                }



                //if (this.DefaultViewModel.TryGetValue("Results", out results) &&
                //    (resultsCollection = results as ICollection) != null &&
                //    resultsCollection.Count != 0)
                //{
                //    VisualStateManager.GoToState(this, "ResultsFound", true);
                //    return;
                //}
            }

            // Display informational text when there are no search results.
            VisualStateManager.GoToState(this, "NoResultsFound", true);
        }

        /// <summary>
        /// Invoked when a filter is selected using a RadioButton when not snapped.
        /// </summary>
        /// <param name="sender">The selected RadioButton instance.</param>
        /// <param name="e">Event data describing how the RadioButton was selected.</param>
        void Filter_Checked(object sender, RoutedEventArgs e)
        {
            // Mirror the change into the CollectionViewSource used by the corresponding ComboBox
            // to ensure that the change is reflected when snapped
            if (filtersViewSource.View != null)
            {
                var filter = (sender as FrameworkElement).DataContext;
                filtersViewSource.View.MoveCurrentTo(filter);
            }
        }

        /// <summary>
        /// View model describing one of the filters available for viewing search results.
        /// </summary>
        private sealed class Filter : Day6_SearchContract.Common.BindableBase
        {
            private String _name;
            private int _count;
            private bool _active;

            public Filter(String name, int count, bool active = false)
            {
                this.Name = name;
                this.Count = count;
                this.Active = active;
            }

            public override String ToString()
            {
                return Description;
            }

            public String Name
            {
                get { return _name; }
                set { if (this.SetProperty(ref _name, value)) this.OnPropertyChanged("Description"); }
            }

            public int Count
            {
                get { return _count; }
                set { if (this.SetProperty(ref _count, value)) this.OnPropertyChanged("Description"); }
            }

            public bool Active
            {
                get { return _active; }
                set { this.SetProperty(ref _active, value); }
            }

            public String Description
            {
                get { return String.Format("{0} ({1})", _name, _count); }
            }
        }
    }
}
