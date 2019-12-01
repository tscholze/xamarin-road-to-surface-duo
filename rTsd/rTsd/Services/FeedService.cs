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
    public class FeedService : IElementService<Post>
    {
        /// <summary>
        /// Article feed endpoint.
        /// </summary>
        private const string FEED_ENDPOINT = "https://www.drwindows.de/news/feed";

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
        ///     - For production, use `DependencyService
        /// </summary>
        /// <param name="cachedPosts">`List of already cached posts.</param>
        public FeedService(List<Post> cachedPosts)
        {
            this.cachedPosts = cachedPosts;
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
                    CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore)
                };

                // Download rss file.
                var rssString = client.DownloadString(FEED_ENDPOINT);

                // Setup xml document parser.
                XDocument doc = XDocument.Parse(rssString);
                XNamespace purl = "http://purl.org/rss/1.0/";
                XNamespace content = "http://purl.org/rss/1.0/modules/content/";
                XNamespace dc = "http://purl.org/dc/elements/1.1/";

                // Parse list of posts.
                var channel = doc.Root.Element("channel");
                var items = channel.Elements("item");
                var posts = items.Select(item => new Post
                {
                    Id = item.Element("guid").Value,
                    Title = item.Element("title").Value,
                    LinkSource = item.Element("link").Value,
                    Content = item.Element(content + "encoded").Value,
                    ImageSource = Regex.Match(item.Element("description").Value, "<img.+?src=[\"'](.+?)[\"'].*?>", RegexOptions.IgnoreCase).Groups[1].Value
                }).ToList();

                // Dispose web client.
                client.Dispose();

                // Store locally
                cachedPosts = posts;

                // Return created posts from parsed document.
                return posts;
            });

            return await task.ConfigureAwait(false);
        }

        public Post GetById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
