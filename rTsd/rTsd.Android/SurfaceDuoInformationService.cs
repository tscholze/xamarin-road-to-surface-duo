using Android.App;
using Microsoft.Device.Display;
using rTsd.Droid;
using rTsd.Services;

[assembly: Xamarin.Forms.Dependency(typeof(SurfaceDuoInformationService))]
namespace rTsd.Droid
{
    public class SurfaceDuoInformationService : ISurfaceDuoInformationService
    {
        #region Private member

        private bool isDuo = false;

        #endregion

        #region ISurfaceDuoInformationService

        public bool IsDuo()
        {
            return isDuo;
        }

        #endregion

        #region Init

        public SurfaceDuoInformationService()
        {
            isDuo = ScreenHelper.IsDualScreenDevice(Application.Context);
        }

        #endregion
    }
}