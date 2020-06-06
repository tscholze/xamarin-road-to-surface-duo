using Xamarin.Forms.DualScreen;

namespace rTsd.Utils
{
    /// <summary>
    /// Custom extenions for the DualScreenInfo object.
    /// </summary>
    public static class DualScreenInfoExtensions
    {
        /// <summary>
        /// Determines if the app is running on a Surface Duo
        /// device.
        /// </summary>
        /// <param name="dualScreenInfo">Information</param>
        /// <returns>True if it runs on a Surface Duo.</returns>
        public static bool IsDuo(this DualScreenInfo dualScreenInfo)
        {
            // TODO: Fix broken feature.
            // https://github.com/tscholze/xamarin-road-to-surface-duo/issues/15
            return true;
        }
    }
}