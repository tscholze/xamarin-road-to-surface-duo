using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace rTsd.ViewModels
{
    /// <summary>
    /// ViewModel of the AboutPage.
    /// </summary>
    public class AboutViewModel : BaseViewModel
    {
        #region Public member

        private bool isTwitterFeatureEnabled;
        /// <summary>
        /// Determines if the user has the Twitter feature enabled.
        /// </summary>
        public bool IsTwitterFeatureEnabled
        {
            get { return isTwitterFeatureEnabled; }
            set 
            {
                Preferences.Set(ENABLE_TWITTER_FEED_PREF_KEY, value);
                SetProperty(ref isTwitterFeatureEnabled, value); 
            }
        }

        /// <summary>
        /// Command to open the GitHub repostiory with the system's browser.
        /// </summary>
        public ICommand OpenGitHubWebCommand { get; }

        /// <summary>
        /// Command to open drwindows.de with the system's browser.
        /// </summary>
        public ICommand OpenDrWindowsWebCommand { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// 
        /// Will setup required fields.
        /// </summary>
        public AboutViewModel() : base()
        {
            // Set checkbox value, if not set, use fale.
            IsTwitterFeatureEnabled = Preferences.Get(ENABLE_TWITTER_FEED_PREF_KEY, false);

            // Setup commands.
            OpenGitHubWebCommand = new Command(() => OpenBrowserToUrlAsync("https://www.github.com/tscholze/xamarin-road-to-surface-duo"));
            OpenDrWindowsWebCommand = new Command(() => OpenBrowserToUrlAsync("https://www.drwindows.de/news/"));
        }

        #endregion
    }
}