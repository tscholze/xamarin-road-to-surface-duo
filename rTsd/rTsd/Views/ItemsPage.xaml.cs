using System.ComponentModel;
using Xamarin.Forms;
using rTsd.ViewModels;

namespace rTsd.Views
{
    /// <summary>
    /// ItemsPage is responsible for rendering a list of 
    /// items.
    /// 
    /// A tap on an item will lead to a detail page navigation.
    /// </summary>
    [DesignTimeVisible(false)]
    public partial class ItemsPage : ContentPage
    {
        #region Private member

        /// <summary>
        /// Underlying view model
        /// </summary>
        readonly ItemsViewModel viewModel;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// Will set and load required information.
        /// </summary>
        public ItemsPage()
        {
            // Initialize ui.
            InitializeComponent();

            // Set binding context to the created view model.
            BindingContext = viewModel = new ItemsViewModel();

            // Setup events.
            viewModel.OnTweetsUpdateScrollPostionRequested += ViewModel_OnTweetsUpdateScrollPostionRequested;

            // Inital items loading.
            viewModel.LoadItemsCommand.Execute(null);
            viewModel.LoadTweetsCommand.Execute(null);
        }

        #endregion

        #region Private helper

        /// <summary>
        /// Will be raised by the view model if a position update of the tweet ticker view is requested.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event args.</param>
        private void ViewModel_OnTweetsUpdateScrollPostionRequested(object sender, TweetsUpdateScrollPostionRequestedEventArgs e)
        {
            TweetsCarouselView.ScrollTo(e.ToTweet);
        }

        #endregion
    }
}