using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace rTsd.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            // Set title
            Title = "About";

            // Opens the GitHub website
            OpenGitHubWebCommand = new Command(() => Device.OpenUri(new Uri("https://github.com/tscholze/xamarin-road-to-surface-duo")));
        }

        /// <summary>
        /// Command to open the web browser
        /// </summary>
        public ICommand OpenGitHubWebCommand { get; }
    }
}