using rTsd.Models;
using rTsd.Services;
using rTsd.Utils;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.DualScreen;

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
        public static IElementService<Post> FeedService => DependencyService.Get<IElementService<Post>>();

        /// <summary>
        /// Twitter to RSS middleware service.
        /// 
        /// Injected by the `DependencyService`.
        /// See `App.cs` for the setup process.
        /// </summary>
        public static IElementService<Tweet> TwitterService => DependencyService.Get<IElementService<Tweet>>();

        /// <summary>
        /// YouTube service.
        /// 
        /// Injected by the `DependencyService`.
        /// See `App.cs` for the setup process.
        /// </summary>
        public static IElementService<Video> YoutubeService => DependencyService.Get<IElementService<Video>>();

        private GridLength secondColumnWidth;

        /// <summary>
        /// Gets the width of the second (optional) column.
        /// </summary>
        public GridLength SecondColumnWidth
        {
            get { return secondColumnWidth; }
            private set { SetProperty(ref secondColumnWidth, value); }
        }

        private List<Tweet> tweets;

        /// <summary>
        /// Twitter items to display.
        /// 
        /// On set, it will be also update the bindable 
        /// `NumberOfTweets` property.
        /// </summary>
        public List<Tweet> Tweets
        {
            get { return tweets; }
            private set
            {
                SetProperty(ref tweets, value);
                NumberOfTweets = tweets.Count;
            }
        }

        private Tweet currentTweet;

        /// <summary>
        /// Selected post in list view.
        /// </summary>
        public Tweet CurrentTweet
        {
            get { return currentTweet; }
            set { SetProperty(ref currentTweet, value); }
        }

        private int numberOfTweets;

        /// <summary>
        /// Number of tweets.
        /// Required due to the data transformation for carousel view.
        /// </summary>
        public int NumberOfTweets
        {
            get { return numberOfTweets; }
            set { SetProperty(ref numberOfTweets, value); }
        }

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

        private List<Video> videos;

        /// <summary>
        /// Videos to display.
        /// </summary>
        public List<Video> Videos
        {
            get { return videos; }
            private set { SetProperty(ref videos, value); }
        }

        private Video selectedVideo;

        /// <summary>
        /// Selected video in list view.
        /// </summary>
        public Video SelectedVideo
        {
            get { return selectedVideo; }
            set { SetProperty(ref selectedVideo, value); }
        }

        /// <summary>
        /// Will trigger a navigation push to the detail page if list's feed item.
        /// </summary>
        public ICommand ItemSelectedCommand { get; private set; }

        /// <summary>
        /// Will trigger a navigation push to the detail page if list's video.
        /// </summary>
        public ICommand VideoSelectedCommand { get; private set; }

        /// <summary>
        /// Will trigger the shell's flyout to be presented or hidden.
        /// </summary>
        public ICommand ShowShellFlyoutCommand { get; private set; }

        /// <summary>
        /// Will trigger a tweets (re-) load.
        /// </summary>
        public ICommand LoadTweetsCommand { get; private set; }

        /// <summary>
        /// Will trigger an item (re-) load.
        /// </summary>
        public ICommand LoadItemsCommand { get; private set; }

        /// <summary>
        /// Will trigger a videos (re-) load.
        /// </summary>
        public ICommand LoadVideosCommand { get; private set; }

        /// <summary>
        /// Command to the open tweet in system's browser.
        /// </summary>
        public ICommand OpenTwitterWebCommand { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void ItemSelectedEventHandler(object sender, ItemSelectedEventArgs e);

        /// <summary>
        /// 
        /// </summary>
        public event ItemSelectedEventHandler ItemSelected;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ItemsViewModel()
        {
            // Setup default values.
            Items = new List<Post>();

            // Check if app runs on a Duo.
            if(DualScreenInfo.Current.HingeBounds.Width != 0)
            {
                SecondColumnWidth = new GridLength(1, GridUnitType.Star);
            }
            else
            {
                SecondColumnWidth = new GridLength(0, GridUnitType.Absolute);
            }

            // Setup commands.
            LoadTweetsCommand = new Command(() => LoadTweetsAsync());
            LoadItemsCommand = new Command(() => LoadArticlesAsync());
            LoadVideosCommand = new Command(() => LoadVideosAsync());

            ItemSelectedCommand = new Command<object>((item) => NavigateToDetailPage(item));
            ShowShellFlyoutCommand = new Command(() => ShowShellFlyout());
        }

        #endregion

        #region Private helper methods

        /// <summary>
        /// Will navigate to the detail page for given post.
        /// </summary>
        /// <param name="post">Post to show in detail.</param>
        private void NavigateToDetailPage(object obj)
        {
            // Ensure required post is set.
            if (obj == null) return;

            ItemSelectedEventArgs eventArgs = new ItemSelectedEventArgs();      
            if (obj is Post post)
            {
                // Deselect selected post.
                SelectedPost = null;

                // Build event args.
                eventArgs.ItemId = post.Id;
                eventArgs.ItemViewModel = new ItemViewModel(post);
            }
            else if(obj is Video video)
            {
                SelectedVideo = null;
                eventArgs.ItemId = video.Id;
                eventArgs.ItemViewModel = new VideoViewModel(video);
            }

            // Raise event.
            ItemSelected(this, eventArgs);
        }

        /// <summary>
        /// Will show or hide the Flyout.
        /// </summary>
        private void ShowShellFlyout()
        {
            Shell.Current.FlyoutIsPresented = !Shell.Current.FlyoutIsPresented;
        }

        /// <summary>
        /// Loads twitter from the service and updates th UI.
        /// </summary>
        private async void LoadTweetsAsync()
        {
            // Update binded Tweets member with service-based tweets.
            Tweets = await TwitterService.GetAllAsync().ConfigureAwait(true);
        }

        /// <summary>
        /// Loads items from the service and updates th UI.
        /// </summary>
        private async void LoadArticlesAsync()
        {
            // Update binded feed item member with service-based items.
            Items = await FeedService.GetAllAsync().ConfigureAwait(true);
        }

        /// <summary>
        /// Loads twitter from the service and updates th UI.
        /// </summary>
        private async void LoadVideosAsync()
        {
            // Update binded video member with service-based videos.
            Videos = await YoutubeService.GetAllAsync().ConfigureAwait(true);
        }

        #endregion
    }
}