using System.ComponentModel;
using Xamarin.Forms;
using rTsd.ViewModels;

namespace rTsd.Views
{
    /// <summary>
    /// ItemDetailPage is responsible for rendering a detail
    /// view of given post.
    /// </summary>
    [DesignTimeVisible(false)]
    public partial class ItemDetailPage : ContentPage
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// Will set and load required information.
        /// </summary>
        /// 
        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            // In this scenario, we set the given view model before we
            // init the UI.
            BindingContext = viewModel;
            InitializeComponent();
        }

        #endregion
    }
}