using rTsd.Models;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace rTsd.ViewModels
{
    /// <summary>
    /// ViewModel of the ItemViewPage.
    /// </summary>
    public class ItemsViewModel : BaseViewModel
    {
        #region Public properties 

        private List<Post> items;

        /// <summary>
        /// Feed items to display.
        /// </summary>
        public List<Post> Items
        {
            get { return items; }
            private set { SetProperty(ref items, value); }
        }

        /// <summary>
        /// Will trigger an item (re-) load.
        /// </summary>
        public ICommand LoadItemsCommand { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// 
        /// Will setup required fields.
        /// </summary>
        public ItemsViewModel()
        {
            // Setup default values.
            Items = new List<Post>();

            // Setup commands.
            LoadItemsCommand = new Command(() => LoadArticlesAsync());
        }

        #endregion

        #region Private helper methods

        /// <summary>
        /// Loads items from the service and updates th UI.
        /// </summary>
        private async void LoadArticlesAsync()
        {
            // Update bindined Items member with service-based items.
            Items = await FeedService.GetAllAsync().ConfigureAwait(true);
        }

        #endregion
    }
}