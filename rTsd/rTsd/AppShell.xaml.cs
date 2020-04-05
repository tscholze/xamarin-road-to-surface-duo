
using rTsd.Resources.Resx;
using rTsd.Views;
using Xamarin.Forms;

namespace rTsd
{
    /// <summary>
    /// App's shell.
    /// 
    /// Master ui container of the app. 
    /// Used in `App.cs` as entry point.
    /// </summary>
    public partial class AppShell : Shell
    {

        #region Constructor

        /// <summary>
        /// Constructor.
        /// 
        /// It will setup navigation related members.
        /// </summary>
        public AppShell()
        {
            InitializeComponent();

            // Add news page to shell navigation
            var news = new ShellSection { Title = AppResources.NavigationNewsTitle, Icon = "icon_blog.png" };
            news.Items.Add(new Views.MasterDetailPage(new ViewModels.ItemsViewModel()));

            // Add about page to shell navigation
            var about = new ShellSection { Title = AppResources.NavigationAboutTitle, Icon = "icon_about.png" };
            about.Items.Add(new AboutPage());

            // Add all sections to shell.
            RootShell.Items.Add(news);
            RootShell.Items.Add(about);
        }

        #endregion
    }
}
