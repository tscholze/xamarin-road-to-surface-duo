using System.Collections.Generic;

namespace rTsd.Models
{
    /// <summary>
    /// Container data model of a feed for posts.
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

        /// <summary>
        /// Link source to the post itself.
        /// </summary>
        public string LinkSource { get; set; }

        /// <summary>
        /// First image (hopefully the article image) of the post.
        /// </summary>
        public string ImageSource { get; set; }

        /// <summary>
        /// Content of the post.
        /// </summary>
        public string Content { get; set; }
    }
}
