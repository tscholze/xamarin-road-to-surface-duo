using System.Windows.Input;

namespace rTsd.ViewModels
{
    /// <summary>
    /// This interface describes required members that are required for a detail
    /// page view model.
    /// </summary>
    interface IDetailPageViewModel
    {
        /// <summary>
        /// Determines if the hero view should be shown.
        /// </summary>
        bool ShowHeroView { get; }

        /// <summary>
        /// Command to the video in system's browser.
        /// </summary>
        ICommand ShareCommand { get; }

        /// <summary>
        /// Command to open the system's share sheet.
        /// </summary>
        ICommand OpenWebCommand { get; }
    }
}
