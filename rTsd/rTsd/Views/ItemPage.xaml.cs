using System.ComponentModel;
using Xamarin.Forms;
using rTsd.ViewModels;
using System;
using Xamarin.Essentials;

namespace rTsd.Views
{
    /// <summary>
    /// ItemPage is responsible for rendering a detail
    /// view of given item.
    /// </summary>
    [DesignTimeVisible(false)]
    public partial class ItemPage : ContentPage
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// Will set and load required information.
        /// 
        /// Accepted view model types:
        ///     - ItemsViewModel
        ///     - VideoViewModel
        ///     
        /// Elsewise:
        ///     - An expection will be thrown.
        /// </summary>
        public ItemPage(object viewModel)
        {
            // Init components with now set BindingContext.
            InitializeComponent();

            // Set binding context according to its view model.
            if (viewModel is ItemViewModel itemViewModel)
            {
                BindingContext = itemViewModel;
                HeroView.IsVisible = true;
            }
            else if (viewModel is VideoViewModel videoViewModel)
            {
                BindingContext = videoViewModel;
                HeroView.IsVisible = false;
            }
            else
            {
                throw new NotImplementedException();
            }

            // Setup custom navigation handling of webview.
            EmbeddedWebView.Navigating += EmbeddedWebView_Navigating;
        }

        #endregion

        #region Event handler

        /// <summary>
        /// Raised on each navigation event of the embedded webview.
        /// </summary>
        /// <param name="sender">Underlying webview.</param>
        /// <param name="e">Event args.</param>
        private async void EmbeddedWebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            // Only handle external links.
            // This will ingore relative links like (css/mystyle.css).
            //
            // Besides embedded Youtube videos which are ignored, too.
            if (!e.Url.StartsWith("http", StringComparison.InvariantCulture) ||
                e.Url.Contains("watch_popup") ||
                e.Url.Contains("embed"))
            {
                return;
            }

            // Open url in system's browser.
            await Browser.OpenAsync(new Uri(e.Url)).ConfigureAwait(false);

            // Cancel the embedded webview navigation.
            e.Cancel = true;
        }

        #endregion
    }
}