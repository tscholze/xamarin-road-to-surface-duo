using Xamarin.Forms;
using rTsd.Services;

namespace rTsd
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            // Setup CDIs
            DependencyService.Register<FeedService>();

            // Setup entry ui point.
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
