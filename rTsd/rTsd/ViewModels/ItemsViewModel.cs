using rTsd.Models;
using rTsd.Services;
using rTsd.Views;
using System;
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

        /// <summary>
        /// Feed service.
        /// 
        /// Injected by the `DependencyService`.
        /// See `App.cs` for the setup process.
        /// </summary>
        public IElementService<Post> FeedService => DependencyService.Get<IElementService<Post>>();

        /// <summary>
        /// Twitter to RSS middleware service.
        /// 
        /// Injected by the `DependencyService`.
        /// See `App.cs` for the setup process.
        /// </summary>
        public IElementService<Tweet> TwitterService => DependencyService.Get<IElementService<Tweet>>();

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

        private List<Tweet> tweets;
        public List<Tweet> Tweets
        {
            get { return tweets; }
            set { SetProperty(ref tweets, value); }
        }

        private Tweet selectedTweet;
        public Tweet SelectedTweet
        {
            get { return selectedTweet; }
            set { SetProperty(ref selectedTweet, value); }
        }


        /// <summary>
        /// Will trigger a navigation push to the detail page if list's item.
        /// </summary>
        public ICommand NavigateToDetailPageCommand { get; private set; }

        /// <summary>
        /// Will trigger the shell's flyout to be presented or hidden.
        /// </summary>
        public ICommand ShowShellFlyoutCommand { get; private set; }

        /// <summary>
        /// Will trigger an item (re-) load.
        /// </summary>
        public ICommand LoadItemsCommand { get; private set; }

        /// <summary>
        /// Will trigger a tweets (re-) load.
        /// </summary>
        public ICommand LoadTweetsCommand { get; private set; }

        /// <summary>
        /// Command to the open tweet in system's browser.
        /// </summary>
        public ICommand OpenTwitterWebCommand { get; }

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
            LoadTweetsCommand = new Command(() => LoadTweetsAsync());

            NavigateToDetailPageCommand = new Command<Post>((post) => NavigateToDetailPage(post));
            OpenTwitterWebCommand = new Command<Tweet>((tweet) => OpenTweetInBrowser(tweet));
            ShowShellFlyoutCommand = new Command(() => ShowShellFlyout());
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
        /// Will show or hide the Flyout.
        /// </summary>
        private void ShowShellFlyout()
        {
            Shell.Current.FlyoutIsPresented = !Shell.Current.FlyoutIsPresented;
        }

        private void OpenTweetInBrowser(Tweet tweet)
        {
            // Ensure required post is set.
            if (tweet == null) return;

            // Open tweet's link in browser.
            OpenBrowserToUrl(tweet.LinkSource);

            // Deselect selected tweet.
            SelectedTweet = null;
        }

        /// <summary>
        /// Loads items from the service and updates th UI.
        /// </summary>
        private async void LoadArticlesAsync()
        {
            // Update binded Items member with service-based items.
            Items = await FeedService.GetAllAsync().ConfigureAwait(true);
        }

        /// <summary>
        /// Loads twitter from the service and updates th UI.
        /// </summary>
        private async void LoadTweetsAsync()
        {
            // Update binded Tweets member with service-based tweets.
            Tweets = await TwitterService.GetAllAsync().ConfigureAwait(true);
        }

        #endregion
    }
}