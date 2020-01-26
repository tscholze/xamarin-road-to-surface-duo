
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace rTsd.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    ///<summary>
    /// ItemsView is reponsable for rendering a mutliple
    /// list-based overview of all information.
    /// detail view.
    /// 
    /// Underlying view model:
    ///     - ItemsViewModel
    /// </summary>
    public partial class ItemsView : ContentView
    {
        public ItemsView()
        {
            InitializeComponent();
        }
    }
}