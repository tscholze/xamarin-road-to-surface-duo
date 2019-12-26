using System;

namespace rTsd.Models
{
    /// <summary>
    /// Data model for Tweets that are converted
    /// by the twitrss.me middleware.
    /// </summary>
    public class Tweet
    {
        /// <summary>
        /// GUID of the Tweet.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Title of the Tweet.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Link source to the Tweet itself.
        /// </summary>
        public string LinkSource { get; set; }

        /// <summary>
        /// Publishing date of the tweet.
        /// </summary>
        public DateTime PublishedOn { get; set; }
    }
}
