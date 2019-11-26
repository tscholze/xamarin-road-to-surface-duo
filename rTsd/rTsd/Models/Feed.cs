using System;
using System.Collections.Generic;
using System.Text;

namespace rTsd.Models
{
    /// <summary>
    /// Data model of a feed.
    /// </summary>
    public class Feed
    {
        // TODO: Implement title, last-updated flag

        /// <summary>
        /// Items of the feed.
        /// </summary>
        public IReadOnlyList<Post> Posts { get; private set; }
    }

    /// <summary>
    /// Data model of a feed's post.
    /// </summary>
    public class Post
    {
        /// <summary>
        /// GUID of the post.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Title of the post.
        /// </summary>
        public string Title { get; set; }

        /// Short abstract of the post.
        public string Abstract { get; set; }
    }
}
