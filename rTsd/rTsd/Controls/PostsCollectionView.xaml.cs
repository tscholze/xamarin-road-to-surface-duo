
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace rTsd.Controls
{
    /// <summary>
    /// Subclass of an `CollectionView` to show 
    /// a styled list of blog posts.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostsCollectionView : CollectionView
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public PostsCollectionView()
        {
            InitializeComponent();
        }

        #endregion
    }
}