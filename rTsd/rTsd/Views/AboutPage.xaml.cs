using rTsd.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace rTsd.Views
{
    /// <summary>
    /// AboutPage is responsible for rendering a information
    /// about the app.
    /// </summary>
    [DesignTimeVisible(false)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();

            // Set binding context to the created view model.
            //BindingContext = new AboutViewModel();
        }
    }
}