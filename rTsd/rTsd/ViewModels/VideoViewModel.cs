using rTsd.Models;
using System.Windows.Input;
using Xamarin.Forms;

namespace rTsd.ViewModels
{
    /// <summary>
    /// ViewModel of a video item detail page.
    /// </summary>
    public class VideoViewModel : BaseViewModel
    {
        #region Public member

        private WebViewSource webViewSource;

        /// <summary>
        /// Url-based web source to display the video.
        /// </summary>
        public WebViewSource WebViewSource
        {
            get { return webViewSource; }
            set { SetProperty(ref webViewSource, value); }
        }

        /// <summary>
        /// Command to the video in system's browser.
        /// </summary>
        public ICommand OpenWebCommand { get; }

        /// <summary>
        /// Command to open the system's share sheet.
        /// </summary>
        public ICommand ShareCommand { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="video">Underlying video.</param>
        public VideoViewModel(Video video)
        {
            if (video == null) return;

            WebViewSource = new UrlWebViewSource
            {
                Url = video.VideoSource
            };

            // Setup commands
            OpenWebCommand = new Command(() => OpenBrowserToUrlAsync(video.VideoSource));
            ShareCommand = new Command(() => OpenShareSheetToUrl(video.VideoSource));
        }

        #endregion
    }
}
