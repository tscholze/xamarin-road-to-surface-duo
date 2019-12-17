using rTsd.Models;

namespace rTsd.ViewModels
{
    /// <summary>
    /// ViewModel of the ItemDetailPage.
    /// </summary>
    public class ItemDetailViewModel : BaseViewModel
    {
        #region Public member

        private Post post;

        /// <summary>
        /// Underlying post.
        /// </summary>
        public Post Post
        {
            get { return post; }
            private set { SetProperty(ref post, value); }
        }

        #endregion

        #region

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="post">Underlying post.</param>
        public ItemDetailViewModel(Post post)
        {
            Post = post;
        }

        #endregion
    }
}
