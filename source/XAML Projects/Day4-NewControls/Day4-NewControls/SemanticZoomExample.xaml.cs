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
    public sealed partial class SemanticZoomExample : Day4_NewControls.Common.LayoutAwarePage
    {
        public SemanticZoomExample()
        {
            this.InitializeComponent();
        }

protected override void OnNavigatedTo(NavigationEventArgs e)
{
    base.OnNavigatedTo(e);

    List<Element> elements = new List<Element>();
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

    ElementData.Source = elements;
    CategoryData.Source = from el in elements group el by el.Category into grp orderby grp.Key select grp;
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
