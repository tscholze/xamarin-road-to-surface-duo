
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

            var home = new ShellSection { Title = "News", Icon = "icon_blog.png" };
            if (hingeService.IsDuo)
            {
                home.Items.Add(new DuoTwoPanePage());
            }
            else
            {
                home.Items.Add(new ItemsPage());
            }

            // Add about page to shell navigation
            var about = new ShellSection { Title = "About", Icon = "icon_about.png" };
            about.Items.Add(new AboutPage());

            RootShell.Items.Add(home);
            RootShell.Items.Add(about);
        }
    }
}
