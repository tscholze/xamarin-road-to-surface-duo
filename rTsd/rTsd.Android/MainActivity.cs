
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;

namespace rTsd.Droid
{
    [Activity(
        Label = "rTsd", 
        Icon = "@mipmap/icon", 
        Theme = "@style/MainTheme", 
        MainLauncher = true, 
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation
    )]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        #region App life cycle

        protected override void OnCreate(Bundle savedInstanceState)
        {
            // Ensure Xamarin.Forms depedency service values are setup early.
            HingeService.MainActivity = this;

            // Get layout resources
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            // Basic Android super call.
            base.OnCreate(savedInstanceState);

            // Enable experimental Xamarin.Forms features.
            Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental");

            // Xamarin.Forms inits.
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Xamarin.Forms.Forms.Init(this, savedInstanceState);

            // Load app from the Forms sub project.
            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        #endregion
    }
}