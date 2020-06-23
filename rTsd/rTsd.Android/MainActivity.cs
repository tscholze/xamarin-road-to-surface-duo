
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.DualScreen;
using Microsoft.Device.Display;
using Xamarin.Forms;
using Android.Content;

namespace rTsd.Droid
{
    [Activity(
        Label = "rTsd", 
        Icon = "@mipmap/icon", 
        Theme = "@style/MainTheme", 
        MainLauncher = true, 
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize
    )]
    public class MainActivity : FormsAppCompatActivity
    {

        #region App life cycle

        protected override void OnCreate(Bundle savedInstanceState)
        {
            // Get layout resources
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            // Basic Android super call.
            base.OnCreate(savedInstanceState);

            // Enable experimental Xamarin.Forms features.
            Forms.SetFlags("CollectionView_Experimental");

            // Xamarin.Forms inits.
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Forms.Init(this, savedInstanceState);
            DualScreenService.Init(this);

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