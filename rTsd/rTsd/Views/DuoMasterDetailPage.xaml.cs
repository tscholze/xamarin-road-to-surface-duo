using rTsd.Utils.MicrosoftDuoLibrary;
using rTsd.ViewModels;
using System.ComponentModel;
using Xamarin.Forms.Xaml;

namespace rTsd.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DuoMasterDetailPage : DuoPage
    {
        readonly ItemsViewModel itemsViewModel = new ItemsViewModel();

        public DuoMasterDetailPage()
        {
            InitializeComponent();

            // Setup items view model.
            itemsViewModel.ItemSelected += ItemsViewModel_ItemSelected;

            // Setup panes.
            MasterPane.BindingContext = itemsViewModel;
            DetailPane.BindingContext = new ItemViewModel(null);

            // Request initial data load.
            itemsViewModel.LoadTweetsCommand.Execute(null);
            itemsViewModel.LoadItemsCommand.Execute(null);
        }

        #region Private helper

        private void ItemsViewModel_ItemSelected(object sender, ItemSelectedEventArgs e)
        {
            // Update left-handeled detail page if:
            //  - Is a Duo device
            //  - App is spanned across both screens
            //  - Device is in portrait mode
            if (FormsWindow.IsSpanned && FormsWindow.IsPortrait)
            {
                DetailPane.BindingContext = e.ItemViewModel;
                return;
            }

            // Elsewise, push it onto the navigation stack.
            Navigation.PushAsync(new ItemPage(e.ItemViewModel));
        }

        #endregion

        #region Event handler

        void OnFormsWindowPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(FormsWindow.IsSpanned) || e.PropertyName == nameof(FormsWindow.IsPortrait))
            {

            }
        }

        #endregion
    }
}