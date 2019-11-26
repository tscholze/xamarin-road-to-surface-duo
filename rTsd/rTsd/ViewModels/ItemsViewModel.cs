using rTsd.Models;
using System.Collections.ObjectModel;
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
        
        /// <summary>
        /// Feed items to display.
        /// </summary>
        public ObservableCollection<Post> Items { get; set; }

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
            Items = new ObservableCollection<Post>();

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
            // Clear existing items.
            Items.Clear();

            // Get items from the service.
            var items = await FeedService.GetAllAsync().ConfigureAwait(true);

            // Add each item to the binded property.
            foreach (var item in items)
            {
                Items.Add(item);
            }
        }

        #endregion
    }
}