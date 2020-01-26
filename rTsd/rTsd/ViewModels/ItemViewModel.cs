using rTsd.Models;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Xamarin.Forms;

namespace rTsd.ViewModels
{
    /// <summary>
    /// ViewModel of a feed item detail page.
    /// </summary>
    public class ItemViewModel : BaseViewModel
    {
        #region Public member

        private bool showPlaceholder = false;

        public bool ShowPlaceholder
        {
            get { return showPlaceholder; }
            private set { SetProperty(ref showPlaceholder, value);  }
        }

        private Post post;

        /// <summary>
        /// Underlying post.
        /// </summary>
        public Post Post
        {
            get { return post; }
            private set { SetProperty(ref post, value); }
        }

        private HtmlWebViewSource webViewSource;

        /// <summary>
        /// Rendered html content of the underlying post.
        /// 
        /// The content will be enriched to include some
        /// styling attributes and other html'ish paramter.
        /// </summary>
        public HtmlWebViewSource WebViewSource
        {
            get { return webViewSource; }
            set { SetProperty(ref webViewSource, value); }
        }

        /// <summary>
        /// Command to the post in system's browser.
        /// </summary>
        public ICommand OpenPostWebCommand { get; }

        /// <summary>
        /// Command to open the system's share sheet.
        /// </summary>
        public ICommand ShareCommand { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="post">Underlying post.</param>
        public ItemViewModel(Post post)
        {
            // Ensure required information is set.
            if (post == null)
            {
                ShowPlaceholder = true;
                return;
            }

            // Set UI properties.
            Post = post;

            WebViewSource = new HtmlWebViewSource
            {
                Html = PreparedContentForWebView(post.Content)
            };

            // Setup commands
            OpenPostWebCommand = new Command(() => OpenBrowserToUrlAsync(post.LinkSource));
            ShareCommand = new Command(() => OpenShareSheetToUrl(post.LinkSource));
        }

        #endregion

        #region Private helper

        /// <summary>
        /// Prepares the feed's post's content to be
        /// presented in the embedded webview.
        /// </summary>
        /// <param name="content">Underlying content.</param>
        /// <returns>Prepared html string for webview.</returns>
        private string PreparedContentForWebView(string content)
        {
            // Read prefix container from file.
            var assembly = typeof(ItemViewModel).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.Resources.container.html");

            // Ennsure a file and stream has been found.
            if (stream == null) return string.Empty;

            // Prefill result with prefix.
            StreamReader reader = new StreamReader(stream);
            var result = reader.ReadToEnd();

            // Cleanup reader.
            reader.Dispose();

            // Strip the embedded article image form the content
            content = Regex.Replace(content, @"<div*>(.+?)</div>", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Singleline);

            // Combine prefix and actual content.
            result += content;
            return result;
        }

        #endregion
    }
}
