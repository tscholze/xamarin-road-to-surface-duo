
using Xamarin.Forms;

namespace rTsd.Effects
{
    /// <summary>
    /// Xamarn.Forms Effect for a Dropshadow.
    /// Implement it in plattform-specific classes.
    /// 
    /// Based on:
    ///     - https://github.com/xamarin/xamarin-forms-samples/tree/master/Effects/ShadowEffect
    /// </summary>
    public class ShadowEffect : RoutingEffect
    {
        #region Public member

        /// <summary>
        /// Shadow radius.
        /// </summary>
        public float Radius { get; set; }

        /// <summary>
        /// Shadow color.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Distance / offset on the x-axis.
        /// </summary>
        public float DistanceX { get; set; }

        /// <summary>
        /// Distance / offset on the y-axis.
        /// </summary>
        public float DistanceY { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// Calling base for 
        ///     - `io.github.tscholze.LabelShadowEffect`.
        /// 
        /// Example usage in plattform-specific implementations
        ///     - [assembly: ResolutionGroupName("io.github.tscholze")]
        ///     - [assembly: ExportEffect(typeof(LabelShadowEffect), "LabelShadowEffect")]
        ///     - See `rTsd.Droid.LabelShadowEffect` for Labels
        /// </summary>
        public ShadowEffect() : base("io.github.tscholze.LabelShadowEffect")
        {
        }

        #endregion
    }
}
