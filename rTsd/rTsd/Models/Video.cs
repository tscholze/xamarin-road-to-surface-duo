using System;
using System.Collections.Generic;
using System.Text;

namespace rTsd.Models
{
    public class Video
    {
        #region Private constants

        private const string FALLBACK_IMAGE_SOURCE = "https://www.drwindows.de/news/wp-content/uploads/2017/07/drwindows_intern_neu-660x330.jpg";

        #endregion

        #region Public member

        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string VideoSource { get; set; }

        public string ThumbnailSource { get; set; }

        public string ImageSource
        {
            get
            {
                if (ThumbnailSource == null || ThumbnailSource.Length == 0)
                {
                    return FALLBACK_IMAGE_SOURCE;
                }
                else
                {
                    return ThumbnailSource;
                }
            }
        }

        #endregion
    }
}
