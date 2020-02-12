using rTsd.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.DualScreen;
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
    public partial class MasterDetailPage : ContentPage
    {
        #region Private member

        bool IsSpanned => DualScreenInfo.Current.IsLandscape == false;

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

        protected override void OnAppearing()
        {
           // DualScreenInfo.Current.PropertyChanged += OnFormsWindowPropertyChanged;
           // UpdateContentForViewModel(null);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            DualScreenInfo.Current.PropertyChanged -= OnFormsWindowPropertyChanged;
        }

        void OnFormsWindowPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DualScreenInfo.Current.SpanMode) || e.PropertyName == nameof(DualScreenInfo.Current.IsLandscape))
            {
                UpdateContentForViewModel(null);
            }
        }

        #region Private helper

        private void UpdateContentForViewModel(object itemViewModel)
        {
            // Update left-handeled detail page if:
            //  - Is a Duo device
            //  - App is spanned across both screens
            //  - Device is in portrait mode
            // Elsewise, push it onto the navigation stack.
            if (DualScreenInfo.Current.HingeBounds.IsEmpty == false && IsSpanned)
            {
                DetailPane.BindingContext = itemViewModel;
                return;
            }

            if (itemViewModel == null) return;

            Navigation.PushAsync(new ItemPage(itemViewModel));
        }

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

            UpdateContentForViewModel(e.ItemViewModel);
        }

        #endregion
    }
}