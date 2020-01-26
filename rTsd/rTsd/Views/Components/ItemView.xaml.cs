
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace rTsd.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
  
    ///<summary>
    /// ItemView is reponsable to render an item-type specifc
    /// detail view.
    /// 
    /// Underlying view model:
    ///     - ItemViewModel
    /// </summary>
    public partial class ItemView : ContentView
    {
        public ItemView()
        {
            InitializeComponent();
        }
    }
}