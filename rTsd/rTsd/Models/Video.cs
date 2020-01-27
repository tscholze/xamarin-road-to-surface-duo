namespace rTsd.Models
{
    /// <summary>
    /// Container data model of a YouTube video.
    /// </summary>
    public class Video
    {
        #region Public member

        /// <summary>
        /// GUID of the video.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Title of the video.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Description of the video.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Url to the video.
        /// </summary>
        public string VideoSource { get; set; }

        /// <summary>
        /// Url to the Tumbnail.
        /// 
        /// Caution: This could be leading to an 404.
        /// </summary>
        public string ThumbnailSource { get; set; }

        #endregion
    }
}
