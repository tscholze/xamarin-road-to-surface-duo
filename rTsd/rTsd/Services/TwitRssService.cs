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
    /// `TwitRssService` is a service that utilizes the twitrss.me
    /// middleware service to convert a Twitter feed into a parseable
    /// xml feed.
    /// 
    /// Note:
    ///     - For a more documented service implementation see <see cref="FeedService"/>.
    ///     - The services is intended to be consumed by a `DependencyService`.
    ///     - Interface methods are comment in the interface itself.
    /// </summary>
    public class TwitRssService : IElementService<Tweet>
    {
        #region Private constants 

        /// <summary>
        /// Twitter to RSS middleware feed endpoint.
        /// </summary>
        private const string FEED_ENDPOINT = "https://twitrss.me/twitter_user_to_rss/?user=drwindows_de";

        /// <summary>
        /// Regexp to find an uri.
        /// </summary>
        private const string URI_REGEX_PATTERN = @"((\w+:\/\/)[-a-zA-Z0-9:@;?&=\/%\+\.\*!'\(\),\$_\{\}\^~\[\]`#|]+)";

        /// <summary>
        /// Maximum number of tweets that should be returned.
        /// </summary>
        private const int MAX_NUMBER_OF_TWEETS = 5;

        #endregion

        #region Private member

        /// <summary>
        /// List of already requested tweets.
        /// </summary>
        private List<Tweet> chachedTweets = new List<Tweet>();

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// 
        /// ! Caution !
        ///     - Only for testing purpose.
        ///     - For production, use `DependencyService`.
        /// </summary>
        /// <param name="chachedTweets">`List of already cached tweets.</param>
        public TwitRssService(List<Tweet> chachedTweets)
        {
            this.chachedTweets = chachedTweets;
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public TwitRssService()
        {
            // Required by depdency register.
            // See `App.cs`.
        }

        #endregion

        #region IElementService

        public async Task<List<Tweet>> GetAllAsync(bool forceReload)
        {
            var task = Task.Run(() =>
            {
                if (forceReload == false && chachedTweets.Count > 0) return chachedTweets;

                WebClient client = new WebClient { CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore) };

                var tweets = XDocument
                                .Parse(client.DownloadString(FEED_ENDPOINT))
                                .Root
                                .Element("channel")
                                .Elements("item")
                                .Select(item => new Tweet
                                {
                                    Id = item.Element("guid").Value.Replace("https://twitter.com/DrWindows_de/status/", string.Empty),
                                    Title = SanitazeTitle(item.Element("title").Value),
                                    LinkSource = item.Element("link").Value
                                })
                                .ToList()
                                .GetRange(0, MAX_NUMBER_OF_TWEETS);

                client.Dispose();

                chachedTweets = tweets;
                return tweets;
            });

            return await task.ConfigureAwait(false);
        }

        Tweet IElementService<Tweet>.GetById(string id)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private helper

        /// <summary>
        /// Sanitizes the title from a given parsed input value.
        /// </summary>
        /// <param name="input">Input string to sanitized.</param>
        /// <returns>Sanitized input.</returns>
        private string SanitazeTitle(string input)
        {
            // Remove trailing links.
            var title = Regex.Replace(input, URI_REGEX_PATTERN, string.Empty).Trim();

            // Replace trailing "- " occurrence.
            // This could be also done via the regexp of above's line of source.
            if (title.EndsWith(" -", StringComparison.InvariantCultureIgnoreCase))
            {
                title = title.Substring(0, input.Length - 2);
            }

            // Returned sanitized title.
            return title;
        }

        #endregion
    }
}
