using rTsd.Models;
using rTsd.Views;
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

        private Post selectedPost;

        /// <summary>
        /// Selected post in list view.
        /// </summary>
        public Post SelectedPost
        {
            get { return selectedPost; }
            set { SetProperty(ref selectedPost, value); }
        }

        /// <summary>
        /// Will trigger a navigation push to the detail page if list's item.
        /// </summary>
        public ICommand NavigateToDetailPageCommand { get; private set; }

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
            NavigateToDetailPageCommand = new Command<Post>((post) => NavigateToDetailPage(post));
            LoadItemsCommand = new Command(() => LoadArticlesAsync());
        }

        #endregion

        #region Private helper methods

        /// <summary>
        /// Will navigate to the detail page for given post.
        /// </summary>
        /// <param name="post">Post to show in detail.</param>
        private async void NavigateToDetailPage(Post post)
        {
            // Ensure required post is set.
            if (post == null) return;

            // Get detail page with required view model set.
            var page = new ItemDetailPage(new ItemDetailViewModel(post));

            // Navigate to the detail page.
            await Application.Current.MainPage.Navigation.PushAsync(page).ConfigureAwait(false);

            // Deselect selected post.
            SelectedPost = null;
        }

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