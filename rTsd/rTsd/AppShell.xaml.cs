
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

        public AppShell()
        {
            InitializeComponent();

            var news = new ShellSection { Title = "News", Icon = "icon_blog.png" };

            // Add underlying content page dependet on if it runs on a 
            // Surface Duo.
            if (hingeService.IsDuo)
            {
                news.Items.Add(new DuoMasterDetailPage());
            }
            else
            {
                news.Items.Add(new ItemsPage());
            }

            // Add about page to shell navigation
            var about = new ShellSection { Title = "About", Icon = "icon_about.png" };
            about.Items.Add(new AboutPage());

            // Add all sections to shell.
            RootShell.Items.Add(news);
            RootShell.Items.Add(about);
        }
    }
}
