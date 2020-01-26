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
        public ItemsPage(ItemsViewModel viewModel)
        {
            // Initialize ui.
            InitializeComponent();

            // Set binding context to the given view model.
            BindingContext = this.viewModel = viewModel;

            // Setup view model.
            this.viewModel.ItemSelected += ViewModel_ItemSelected;

            // Inital items loading.
            this.viewModel.LoadTweetsCommand.Execute(null);
            this.viewModel.LoadItemsCommand.Execute(null);
            this.viewModel.LoadVideosCommand.Execute(null);
        }

        #endregion

        #region Event handler

        private void ViewModel_ItemSelected(object sender, ItemSelectedEventArgs e)
        {
            Navigation.PushAsync(new ItemPage(e.ItemViewModel));
        }

        #endregion
    }
}