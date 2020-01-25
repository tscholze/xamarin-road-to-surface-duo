using System.ComponentModel;
using Xamarin.Forms;
using rTsd.ViewModels;
using System;

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
            if(viewModel is ItemViewModel itemViewModel)
            {
                BindingContext = itemViewModel;
                InitializeComponent();
            }
            else if (viewModel is VideoViewModel videoViewModel)
            {
                BindingContext = videoViewModel;
                InitializeComponent();
            }
            else
            {
                throw new NotImplementedException();
            }
        }
        #endregion
    }
}