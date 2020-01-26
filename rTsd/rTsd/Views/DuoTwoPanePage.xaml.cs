﻿using rTsd.Utils.MicrosoftDuoLibrary;
using rTsd.ViewModels;
using System.ComponentModel;
using Xamarin.Forms.Xaml;

namespace rTsd.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DuoMasterDetailPage : DuoPage
    {
        /// <summary>
        /// Id of the underlying data item of the currently pushed 
        /// detail page.
        /// </summary>
        string pushedDetailPageId = string.Empty;

        /// <summary>
        /// Underlying view model.
        /// </summary>
        readonly ItemsViewModel itemsViewModel = new ItemsViewModel();

        public DuoMasterDetailPage()
        {
            InitializeComponent();

            // Setup items view model.
            itemsViewModel.ItemSelected += ItemsViewModel_ItemSelected;

            // Setup panes.
            MasterPane.BindingContext = itemsViewModel;
            DetailPane.BindingContext = new ItemViewModel(null);

            // Request initial data load.
            itemsViewModel.LoadTweetsCommand.Execute(null);
            itemsViewModel.LoadItemsCommand.Execute(null);
            itemsViewModel.LoadVideosCommand.Execute(null);
        }

        #region Private helper

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

        void OnFormsWindowPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(FormsWindow.IsSpanned) || e.PropertyName == nameof(FormsWindow.IsPortrait))
            {

            }
        }

        #endregion
    }
}