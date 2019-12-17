using rTsd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace rTsd.Services
{
    /// <summary>
    /// A <see cref="IElementService{T}"/> that reads and aprses feed 
    /// information from the web.
    /// 
    /// Interface methods are comment in the interface itself.
    /// </summary>
    public class FeedService : IElementService<Post>
    {
        #region Private constants 

        /// <summary>
        /// Feed endpoint.
        /// </summary>
        private const string FEED_ENDPOINT = "https://www.drwindows.de/news/feed";

        /// <summary>
        /// Fallback for the image source of a post.
        /// </summary>
        private const string IMAGESOURCE_FALLBACK = "https://www.drwindows.de/news/wp-content/themes/drwindows_theme/img/DrWindows-Windows-News.png";

        #endregion

        #region Private member

        /// <summary>
        /// List of already requested posts.
        /// </summary>
        private List<Post> cachedPosts = new List<Post>();

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// 
        /// ! Caution !
        ///     - Only for testing purpose.
        ///     - For production, use `DependencyService`.
        /// </summary>
        /// <param name="cachedPosts">`List of already cached posts.</param>
        public FeedService(List<Post> cachedPosts)
        {
            this.cachedPosts = cachedPosts;
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public FeedService()
        {
            // Required by CDI.
            // See `App.cs`.
        }

        #endregion

        public async Task<List<Post>> GetAllAsync(bool forceReload = false)
        {
            // TODO: Check if real async would be better.
            // TODO: Learn it!
            var task = Task.Run(() =>
            {
                // If no reload is forced and cached posts are available,
                // return cached posts.
                // Else load posts from web service
                if (forceReload == false && cachedPosts.Count > 0)
                {
                    return cachedPosts;
                }

                // Setup web client.
                WebClient client = new WebClient
                {
                    // Ignore chaching.
                    // Always load data from the server instead from the cache.
                    CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore)
                };

                // Download rss file.
                var rssString = client.DownloadString(FEED_ENDPOINT);

                // Setup xml document parser.
                XDocument doc = XDocument.Parse(rssString);
                XNamespace content = "http://purl.org/rss/1.0/modules/content/";

                // Example data structure
                //
                //<xml>
                //  <channel>
                //      <item>
                //          <title>Entry #1</title>
                //      </item>
                //      <item>
                //          n times ...
                //      </item>
                //  </channel>

                // Get channels from the document.
                var channel = doc.Root.Element("channel");

                // Get all `item` entries from the channel.
                var items = channel.Elements("item");

                // Convert found item xml entries into post objects.
                // By mapping element tags to object members.
                // E.g. item.guid -> object.id
                var posts = items.Select(item => new Post
                {
                    Id = item.Element("guid").Value.Replace("https://www.drwindows.de/news/?p=", string.Empty),
                    Title = item.Element("title").Value,
                    LinkSource = item.Element("link").Value,
                    Content = item.Element(content + "encoded").Value,
                    ImageSource = GetImageSourceOutOfContent(item.Element("description").Value)
                }).ToList();

                // Dispose web client.
                client.Dispose();

                // Store locally
                cachedPosts = posts;

                // Return created posts from parsed document.
                return posts;
            });

            // Return async task.
            return await task.ConfigureAwait(false);
        }

        public Post GetById(string id)
        {
            throw new NotImplementedException();
        }

        #region Private helper 

        /// <summary>
        /// Gets the image source out of a content string.
        /// </summary>
        /// <param name="content">Content string of a post.</param>
        /// <returns>Found or fallback image source.</returns>
        private string GetImageSourceOutOfContent(string content)
        {
            // Try to extract the first `<img src=".." /> out of the content string 
            // to use as image source.
            var source = Regex.Match(content, "<img.+?src=[\"'](.+?)[\"'].*?>", RegexOptions.IgnoreCase).Groups[1].Value;

            // If no image found in content, use fallback.
            return source ?? IMAGESOURCE_FALLBACK;
        }

        #endregion
    }
}
