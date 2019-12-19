using rTsd.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace rTsd.Controls
{
    /// <summary>
    /// Represents a hero view with a large image background
    /// and the title of given post.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostHeroView : ContentView
    {
        #region Public member

        /// <summary>
        /// Required bindable property of the control for type of Post.
        /// </summary>
        public static readonly BindableProperty PostProperty = BindableProperty.Create(
            propertyName: "Post",
            returnType: typeof(Post),
            declaringType: typeof(PostHeroView),
            defaultValue: null,
            defaultBindingMode: BindingMode.OneWay,
            propertyChanged: PostPropertyChanged);

        #endregion

        #region Constructor

        public PostHeroView()
        {
            InitializeComponent();
        }

        #endregion

        #region Private helper

        /// <summary>
        /// Triggerd in case of the bindable post member has been changed.
        /// </summary>
        /// <param name="bindable">Underlying control.</param>
        /// <param name="oldValue">Old bind instance.</param>
        /// <param name="newValue">New bind instance.</param>
        private static void PostPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            // Cast parameter to required types.
            var control = (PostHeroView)bindable;
            var post = (Post)newValue;

            // Ensure that all required information set.
            if (control == null || post == null) return;

            // Update UI.
            control.BackgroundImage.Source = post.ImageSource;
            control.TitleLabel.Text = post.Title;
        }

        #endregion
    }
}