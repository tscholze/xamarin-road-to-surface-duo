using rTsd.Utils.MicrosoftDuoLibrary;
using rTsd.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms.Xaml;

namespace rTsd.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    /// <summary>
    /// MasterDetailPage is responsible for rendering a Surface Duo aware
    /// master detail view for given view model.
    /// 
    /// Sub pages:
    ///     - ItemsView
    ///     - ItemView
    ///     - ItemsPage (required for non-spanned mode.)
    /// </summary>
    public partial class MasterDetailPage : DuoPage
    {
        #region Private member

        /// <summary>
        /// Id of the underlying data item of the currently pushed 
        /// detail page.
        /// </summary>
        string pushedDetailPageId = string.Empty;

        /// <summary>
        /// Underlying view model.
        /// </summary>
        readonly ItemsViewModel viewModel;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// 
        /// Will setup required fields.
        /// </summary>
        /// <param name="viewModel">Underlying view model.</param>
        public MasterDetailPage(ItemsViewModel viewModel)
        {
            // Ensure view model is set
            this.viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

            InitializeComponent();

            // Setup items view model.
            this.viewModel.ItemSelected += ItemsViewModel_ItemSelected;

            // Setup panes.
            MasterPane.BindingContext = this.viewModel;
            DetailPane.BindingContext = new ItemViewModel(null);

            // Request initial data load.
            this.viewModel.LoadTweetsCommand.Execute(null);
            this.viewModel.LoadItemsCommand.Execute(null);
            this.viewModel.LoadVideosCommand.Execute(null);
        }

        #endregion

        #region Private helper

        /// <summary>
        /// Will be raised if an item has been selected.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event args.</param>
        private void ItemsViewModel_ItemSelected(object sender, ItemSelectedEventArgs e)
        {
            // Ensure this is a new detail page, if not, do nothing
            if (pushedDetailPageId == e.ItemId) return;

            // Store item id as currently pushed / selected detail item.
            pushedDetailPageId = e.ItemId;

            // Update left-handeled detail page if:
            //  - Is a Duo device
            //  - App is spanned across both screens
            //  - Device is in portrait mode
            // Elsewise, push it onto the navigation stack.
            if (!(FormsWindow.IsSpanned && FormsWindow.IsPortrait))
            {
                Navigation.PushAsync(new ItemPage(e.ItemViewModel));
                return;
            }

            DetailPane.BindingContext = e.ItemViewModel;
        }

        #endregion

        #region Event handler

        /// <summary>
        /// Will be called if the app window changed. 
        /// 
        /// Example:
        ///     - Non spanned to spanned.
        ///     - Rotation.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event args.</param>
        void OnFormsWindowPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(FormsWindow.IsSpanned) || e.PropertyName == nameof(FormsWindow.IsPortrait))
            {
                // TODO: Implement feature that the detail screen does update itself after window changed.
            }
        }

        #endregion
    }
}