
using rTsd.Utils.MicrosoftDuoLibrary;
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
        #region Private member 

        IHingeService hingeService => DependencyService.Get<IHingeService>();

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// 
        /// It wil setup navigation related members.
        /// </summary>
        public AppShell()
        {
            InitializeComponent();

            // Add news page to shell navigation
            var news = new ShellSection { Title = "News", Icon = "icon_blog.png" };
            news.Items.Add(new Views.MasterDetailPage(new ViewModels.ItemsViewModel(hingeService.IsDuo)));

            // Add about page to shell navigation
            var about = new ShellSection { Title = "About", Icon = "icon_about.png" };
            about.Items.Add(new AboutPage());

            // Add all sections to shell.
            RootShell.Items.Add(news);
            RootShell.Items.Add(about);
        }

        #endregion
    }
}
