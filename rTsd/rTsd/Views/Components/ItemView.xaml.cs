
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace rTsd.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    ///<summary>
    /// ItemView is reponsable to render an item-type specifc
    /// detail view.
    /// 
    /// Underlying view model:
    ///     - ItemViewModel
    /// </summary>
    public partial class ItemView : ContentView
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// Will set and load required information.
        /// </summary>
        public ItemView()
        {
            InitializeComponent();

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