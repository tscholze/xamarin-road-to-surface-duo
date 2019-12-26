using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace rTsd.Extensions
{
    /// <summary>
    /// Extends the XAML Image by another way to define the
    /// `Source`.
    /// 
    /// This is used to use "embedded images" in the forms project
    /// rather than in each platform-specifc project.
    /// 
    /// Based on:
    ///     https://www.youtube.com/watch?v=VVpbklb6vDc
    /// </summary>
    [ContentProperty (nameof(Source))]
    public class ImageResourceExtension : IMarkupExtension
    {
        public string Source { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source == null) return null;

            var imageSource = ImageSource.FromResource(Source);
            return imageSource;
        }
    }
}
