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
            // It follows an example on how to inject
            // another service depenent on current set
            // solution run configuration.
            //
            // It will use the MockedFeedService.cs if the
            // DEBUG flag is set.
            //
            // Otherwise it will use the actual service with
            // real life information and requests.
            //
            // For tutorial purpose, this functionality is not
            // set active during the current implementation phase.
//#if DEBUG
            // DependencyService.Register<MockedFeedService>();
//#else
            DependencyService.Register<FeedService>();
            //#endif
            DependencyService.Register<TwitRssService>();
            DependencyService.Register<YouTubeService>();

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
