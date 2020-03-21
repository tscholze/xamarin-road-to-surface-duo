using rTsd.Models;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace rTsd.Controls
{
    /// <summary>
    /// Subclass of an `CarouselView` to show a ticker-like
    /// auto-scrolling view of binded tweets.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TweetsCarouselView : CarouselView
    {
        #region Public member

        /// <summary>
        /// Required bindable property of the control for the number of tweets.
        /// </summary>
        public static readonly BindableProperty NumberOfTweetsProperty = BindableProperty.Create(
            propertyName: "NumberOfTweets",
            returnType: typeof(int),
            declaringType: typeof(TweetsCarouselView),
            defaultValue: null,
            defaultBindingMode: BindingMode.OneWay,
            propertyChanged: NumberOfTweetsTweetsPropertyChanged);

        #endregion

        #region Private member

        /// <summary>
        /// Number of tweets / items of the view.
        /// </summary>
        private int NumberOfTweets = 0;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// Will also start the timer to update the current item of the carousel view.
        /// </summary>
        public TweetsCarouselView()
        {
            InitializeComponent();

            // Setup timer.
            Device.StartTimer(TimeSpan.FromSeconds(3), OnTweetScrollTimerTicked);
        }

        #endregion

        /// <summary>
        /// Event handler that will be raised of the user taps on a tweet cell.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event args.</param>
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (!(CurrentItem is Tweet tweet)) return;
            Browser.OpenAsync(new Uri(tweet.LinkSource), BrowserLaunchMode.SystemPreferred);
        }

        /// <summary>
        /// Raised on every tweet scroll timer tick.
        /// </summary>
        /// <returns>If timer should be cancled.</returns>
        private bool OnTweetScrollTimerTicked()
        {
            // Ensure all required information are set.
            if (ItemsSource == null || NumberOfTweets == 0) return true;

            if (NumberOfTweets == 1)
            {
                Position = 0;
                return true;
            }

            // Handle initial case and select the first one.
            if (CurrentItem == null)
            {
                Position = 0;
                return true;
            }

            // Get next tweet in ever-repeating row.
            Position = ((Position + 1) % (NumberOfTweets - 1));

            // Done.
            return true;
        }

        /// <summary>
        /// Raised of the NumberOfTweets got a new binding value.
        /// </summary>
        /// <param name="bindable">TweetsCarouselView itself.</param>
        /// <param name="oldValue">Unused old value.</param>
        /// <param name="newValue">New value.</param>
        private static void NumberOfTweetsTweetsPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is TweetsCarouselView view)) return;
            if (!(newValue is int numberOfTweets)) return;

            view.NumberOfTweets = numberOfTweets;
        }
    }
}