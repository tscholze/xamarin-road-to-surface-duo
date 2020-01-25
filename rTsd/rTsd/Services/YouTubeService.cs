using rTsd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace rTsd.Services
{
    public class YouTubeService : IElementService<Video>
    {
        #region Private constants 

        /// <summary>
        /// Feed endpoint.
        /// </summary>
        private const string FEED_ENDPOINT = "https://www.youtube.com/feeds/videos.xml?user=DrWindowsTV";

        #endregion

        #region Private member

        /// <summary>
        /// List of already requested posts.
        /// </summary>
        private List<Video> cachedVideos = new List<Video>();

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
        public YouTubeService(List<Video> cachedVideos)
        {
            this.cachedVideos = cachedVideos;
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public YouTubeService()
        {
            // Required by depdency register.
            // See `App.cs`.
        }

        #endregion

        #region IElementService

        public async Task<List<Video>> GetAllAsync(bool forceReload = false)
        {
            var task = Task.Run(() =>
            {
                if (forceReload == false && cachedVideos.Count > 0) return cachedVideos;

                WebClient client = new WebClient { CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore) };
                XNamespace xmlns = "http://www.w3.org/2005/Atom";
                XNamespace yt = "http://www.youtube.com/xml/schemas/2015";
                XNamespace media = "http://search.yahoo.com/mrss/";

                var videos = XDocument
                                .Parse(client.DownloadString(FEED_ENDPOINT))
                                .Root
                                .Elements(xmlns + "entry")
                                .Select(item => new Video
                                {
                                    Id = item.Element(yt + "videoId").Value,
                                    Title = item.Element(xmlns + "title").Value,
                                    Description = item.Element(media + "group").Element(media + "description").Value,
                                    VideoSource = item.Element(media + "group").Element(media + "content").Attribute("url").Value,
                                    ThumbnailSource = item.Element(media + "group").Element(media + "thumbnail").Attribute("url").Value.Replace("hqdefault", "maxresdefault"),

                                })
                                .ToList();

                cachedVideos = videos;
                return videos;
            });

            return await task.ConfigureAwait(false);
        }

        public Video GetById(string id)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
