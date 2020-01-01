using rTsd.Models;
using System;

namespace rTsd.ViewModels
{
    /// <summary>
    /// Event args of the OnTweetsUpdateScrollPostionRequested event.
    /// </summary>
    public class TweetsUpdateScrollPostionRequestedEventArgs : EventArgs
    {
        #region Public member

        /// <summary>
        /// Tweet that should be scrolled to.
        /// </summary>
        public Tweet ToTweet { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="toTweet">Tweet that should be scrolled to.</param>
        public TweetsUpdateScrollPostionRequestedEventArgs(Tweet toTweet)
        {
            ToTweet = toTweet;
        }

        #endregion
    }
}